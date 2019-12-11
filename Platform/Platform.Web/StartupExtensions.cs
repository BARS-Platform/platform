using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Platform.Domain.DomainServices;
using Platform.Fatabase;
using Platform.Fodels;
using Platform.Fodels.Models;
using Platform.Services.Common;
using Platform.Services.Handlers;
using Platform.Services.Helpers;
using Platform.Services.Requirements;
using Platform.Services.Services;
using Platform.Web.Services.SwaggerServices;

namespace Platform.Web
{
    public static class StartupExtensions
    {
        private static List<Role> _registeredRoles;
        private static List<Permission> _registeredPermissions;
        private static Dictionary<string, List<string>> _registeredRolePermissions;
        
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtOptions.Issuer,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtOptions.Key)),
                    ValidateIssuerSigningKey = true
                };
            });
        }

        public static void AddPoliciesAuthorization(this IServiceCollection services)
        {
            _registeredRoles = new List<Role>
            {
                new Role(RoleNamesHelper.Admin, "Администратор"),
                new Role(RoleNamesHelper.User, "Пользователь")
            };

            _registeredPermissions = new List<Permission>
            {
                new Permission(PermissionNamesHelper.ViewModels, "Просмотр моделей"),
                new Permission(PermissionNamesHelper.ViewDictionaries, "Просмотр справочников"),
                new Permission(PermissionNamesHelper.ViewAdmin, "Просмотр администрирования"),
                new Permission(PermissionNamesHelper.RoleView, "Просмотр Ролей"),
                new Permission(PermissionNamesHelper.RoleEdit, "Изменение Ролей"),
                new Permission(PermissionNamesHelper.PermissionView, "Просмотр Разрешений"),
                new Permission(PermissionNamesHelper.PermissionEdit, "Изменение Разрешений")
            };
            
            _registeredRolePermissions = new Dictionary<string, List<string>>
            {
                {
                    RoleNamesHelper.Admin, new List<string>
                    {
                        PermissionNamesHelper.ViewModels,
                        PermissionNamesHelper.ViewDictionaries,
                        PermissionNamesHelper.ViewAdmin,
                        PermissionNamesHelper.RoleView,
                        PermissionNamesHelper.PermissionView,
                        PermissionNamesHelper.RoleEdit,
                        PermissionNamesHelper.PermissionEdit
                    }
                },
                
                {
                    RoleNamesHelper.User, new List<string>
                    {
                        PermissionNamesHelper.ViewModels,
                        PermissionNamesHelper.ViewDictionaries
                    }
                }
            };
            
            services.AddAuthorization(options =>
            {
                _registeredRoles.ForEach(options.RegisterRole);
                _registeredPermissions.ForEach(options.RegisterPermission);
            });
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationConfiguration>();

            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<IRepository, BaseRepository>();

			services.AddSingleton<PasswordCheckerService>();
			services.AddScoped<TokenService>();
            services.AddScoped<UserDomainService>();
            services.AddScoped<AddressDomainService>();

            services.AddSingleton<PlatformSwaggerSchemasCustomizer>();

            services.AddTransient<IAuthorizationHandler, RoleHandler>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            services.AddScoped<PermissionService>();
            services.AddTransient<MenuService>();
            services.AddScoped<RoleService>();
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Startup.SwaggerConfigurationName, new OpenApiInfo()
                {
                    Title = "Platform Swagger API",
                    Version = Startup.SwaggerConfigurationName
                });
                c.DocumentFilter<PlatformSwaggerDocumentFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header, Description = "Please insert JWT in next format: Bearer *token*",
                    Name = "Authorization", Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                            {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}},
                        new string[] { }
                    }
                });
                
#pragma warning disable 618
                c.DescribeAllEnumsAsStrings();
#pragma warning restore 618
                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        public static void CheckRegisteredRolesForExisting(this IServiceProvider serviceProvider)
        {
            var repository = serviceProvider.GetService<IRepository>();
            var roleNames = _registeredRoles.Select(x => x.RoleName);
            var existingRoles = repository
                .FindAllByPredicate<Role>(role => roleNames.Contains(role.RoleName))
                .ToList();
            
            _registeredRoles
                .Where(x => !existingRoles.Contains(x))
                .ToList()
                .ForEach(role => repository.Create(role));
        }
        
        public static void CheckRegisteredPermissionsForExisting(this IServiceProvider serviceProvider)
        {
            var repository = serviceProvider.GetService<IRepository>();
            var permissionIds = _registeredPermissions.Select(x => x.PermissionId);
            var existingPermissions = repository
                .FindAllByPredicate<Permission>(perm => permissionIds.Contains(perm.PermissionId))
                .ToList();
            
            _registeredPermissions
                .Where(x => !existingPermissions.Contains(x))
                .ToList()
                .ForEach(permission => repository.Create(permission));
        }
        
        public static void CheckRegisteredRolePermissionsForExisting(this IServiceProvider serviceProvider)
        {
            var repository = serviceProvider.GetService<IRepository>();
            var registeredRoles = _registeredRolePermissions.Select(x => x.Key);

            foreach (var role in registeredRoles)
            {
                var permissionsForRole = _registeredRolePermissions[role];
                var actualRole = repository.FindByPredicate<Role>(x => x.RoleName == role);
                
                var actualPermissions = repository
                        .FindAllByPredicate<RolePermission>(x => x.Role.RoleName == role)
                        .Select(x => x.Permission.PermissionId)
                        .ToList();

                var permissionsForDelete = actualPermissions
                    .Where(x => !permissionsForRole.Contains(x));
                repository
                    .FindAllByPredicate<RolePermission>(x => x.Role.RoleName == role)
                    .Where(x => permissionsForDelete.Contains(x.Permission.PermissionId))
                    .ToList()
                    .ForEach(rolePermission => repository.Delete(rolePermission));

                var notExistingRolePermissions = permissionsForRole
                    .Where(x => !actualPermissions.Contains(x));
                repository
                    .FindAllByPredicate<Permission>(x => notExistingRolePermissions.Contains(x.PermissionId))
                    .ToList()
                    .ForEach(permission => repository.Create(new RolePermission(actualRole, permission)));
            }
        }

        private static void RegisterRole(this AuthorizationOptions options, Role role)
        {
            options.AddPolicy(role.RoleName,
                builder =>
                {
                    builder.Requirements.Add(new RoleRequirement(role.RoleName));
                });
        }

        private static void RegisterPermission(this AuthorizationOptions options, Permission permission)
        {
            options.AddPolicy(permission.PermissionId,
                builder =>
                {
                    builder.Requirements.Add(new PermissionRequirement(permission.PermissionId));
                });
        }
    }
}