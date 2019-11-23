using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
                        PermissionNamesHelper.RoleEdit
                    }
                },
                
                {
                    RoleNamesHelper.User, new List<string>
                    {
                        PermissionNamesHelper.ViewModels,
                        PermissionNamesHelper.ViewDictionaries,
                        PermissionNamesHelper.RoleView,
                        PermissionNamesHelper.PermissionView
                    }
                }
            };
            
            services.AddAuthorization(options =>
            {
                foreach (var role in _registeredRoles)
                {
                    options.RegisterRole(role);
                }
                
                foreach (var permission in _registeredPermissions)
                {
                    options.RegisterPermission(permission);
                }
            });
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationConfiguration>();

            services.AddTransient<ApplicationDbContext>();
            services.AddSingleton(typeof(IRepository), typeof(BaseRepository));

			services.AddSingleton<PasswordCheckerService>();
			services.AddSingleton<TokenService>();
            services.AddSingleton<UserDomainService>();
            services.AddSingleton<AddressDomainService>();

            services.AddSingleton<PlatformSwaggerSchemasCustomizer>();

            services.AddSingleton<IAuthorizationHandler, RoleHandler>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

            services.AddTransient<PermissionService>();
            services.AddTransient<MenuService>();
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

        public static void CheckRegisteredRolesAndPermissionsForExisting(this IServiceProvider serviceProvider)
        {
            var repository = serviceProvider.GetService<IRepository>();
            var roleNames = _registeredRoles.Select(x => x.RoleName);
            var existingRoles = repository
                .FindAllByPredicate<Role>(role => roleNames.Contains(role.RoleName))
                .ToList();
            var notExistingRoles = _registeredRoles
                .Where(x => !existingRoles.Contains(x))
                .ToList();
            
            foreach (var role in notExistingRoles)
            {
                repository.Create(role);
            }
            
            var permissionIds = _registeredPermissions.Select(x => x.PermissionId);
            var existingPermissions = repository
                .FindAllByPredicate<Permission>(perm => permissionIds.Contains(perm.PermissionId))
                .ToList();
            var notExistingPermissions = _registeredPermissions
                .Where(x => !existingPermissions.Contains(x))
                .ToList();

            foreach (var permission in notExistingPermissions)
            {
                repository.Create(permission);
            }
            
            foreach (var rolePermission in _registeredRolePermissions)
            {
                var role = repository.FindByPredicate<Role>(x => x.RoleName == rolePermission.Key);
                var permissions =
                    repository.FindAllByPredicate<Permission>(x => rolePermission.Value.Contains(x.PermissionId));
                foreach (var permission in permissions)
                {
                    repository.Create(new RolePermission(role, permission));
                }
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