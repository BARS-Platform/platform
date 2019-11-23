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
using Platform.Services.Requirements;
using Platform.Services.Services;
using Platform.Web.Services.SwaggerServices;

namespace Platform.Web
{
    public static class StartupExtensions
    {
        private static List<Role> _registeredRoles;
        private static List<Permission> _registeredPermissions;
        
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
                new Role("Admin", "Администратор"),
                new Role("User", "Пользователь")
            };

            _registeredPermissions = new List<Permission>
            {
                new Permission("ViewModels", "Просмотр моделей"),
                new Permission("ViewDictionaries", "Просмотр моделей"),
                new Permission("ViewAdmin", "Просмотр моделей"),
                new Permission("RoleView", "Просмотр Ролей"),
                new Permission("RoleEdit", "Изменение Ролей"),
                new Permission("PermissionView", "Просмотр Разрешений"),
                new Permission("PermissionEdit", "Изменение Разрешений")
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
            var roleRepository = serviceProvider.GetService<IRepository>();
            var roleNames = _registeredRoles.Select(x => x.RoleName);
            var existingRoles = roleRepository
                .FindAllByPredicate<Role>(role => roleNames.Contains(role.RoleName))
                .ToList();
            var notExistingRoles = _registeredRoles
                .Where(x => !existingRoles.Contains(x))
                .ToList();
            
            foreach (var role in notExistingRoles)
            {
                roleRepository.Create(role);
            }

            var permissionRepository = serviceProvider.GetService<IRepository>();
            var permissionIds = _registeredPermissions.Select(x => x.PermissionId);
            var existingPermissions = permissionRepository
                .FindAllByPredicate<Permission>(perm => permissionIds.Contains(perm.PermissionId))
                .ToList();
            var notExistingPermissions = _registeredPermissions
                .Where(x => !existingPermissions.Contains(x))
                .ToList();

            foreach (var permission in notExistingPermissions)
            {
                permissionRepository.Create(permission);
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