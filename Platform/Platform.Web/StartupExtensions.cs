using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FSharp.Collections;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Platform.Domain.Common;
using Platform.Domain.DomainServices;
using Platform.Domain.Services;
using Platform.Fatabase;
using Platform.Fodels;
using Platform.Fodels.Models;
using Platform.Services.Handlers;
using Platform.Services.Helpers;
using Platform.Services.Requirements;
using Platform.Web.Services.SwaggerServices;

namespace Platform.Web
{
    public static class StartupExtensions
    {
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PlatformUser",
                    builder =>
                    {
                        builder.Requirements.Add(new AssertionRequirement(context =>
                            context.User.HasClaim(claim => claim.Type == ClaimTypes.Name)));
                    });

                options.RegisterRole("Admin");
                options.RegisterPermission("RoleView");
                options.RegisterPermission("RoleEdit");
                options.RegisterPermission("PermissionView");
                options.RegisterPermission("PermissionEdit");
            });
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationConfiguration>();

            services.AddTransient<ApplicationDbContext>();
            services.AddSingleton(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddSingleton<PasswordCheckerService>();
            services.AddSingleton<TokenService>();
            services.AddSingleton<UserDomainService>();

            services.AddSingleton<PlatformSwaggerSchemasCustomizer>();

            services.AddSingleton<IAuthorizationHandler, RoleHandler>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
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

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void CheckBaseRolesForExisting(this IServiceProvider serviceProvider)
        {
            const string adminRoleName = "Admin";
            const string adminRoleDescription = "Администратор";
            var adminPermissions = new[]
            {
                new Permission("RoleView", "Просмотр Ролей"),
                new Permission("RoleEdit", "Изменение Ролей"),
                new Permission("PermissionView", "Просмотр Разрешений"),
                new Permission("PermissionEdit", "Изменение Разрешений")
            };
            
            var roleRepository = serviceProvider.GetService<IRepository<Role>>();
            var existingRole = roleRepository
                .FindByPredicate(x => x.RoleName == adminRoleName,
                    x => x.Include(role => role.Permissions));
            
            if (existingRole == null)
            {
                roleRepository.Create(new Role(adminRoleName, adminRoleDescription, adminPermissions));
            }
            else if (existingRole.Permissions == null)
            {
                roleRepository.Delete(existingRole);
                roleRepository.Create(new Role(adminRoleName, adminRoleDescription, adminPermissions));
            }
            else if (existingRole.Permissions.Any(x => !adminPermissions.Contains(x)))
            {
                var permissionRepository = serviceProvider.GetService<IRepository<Permission>>();
                foreach (var permission in existingRole.Permissions)
                {
                    permissionRepository.Delete(permission);
                }
                
                roleRepository.Delete(existingRole);
                roleRepository.Create(new Role(adminRoleName, adminRoleDescription, adminPermissions));
            }
        }

        private static void RegisterRole(this AuthorizationOptions options, string roleName)
        {
            options.AddPolicy(roleName,
                builder => { builder.Requirements.Add(new RoleRequirement(roleName)); });
        }

        private static void RegisterPermission(this AuthorizationOptions options, string permissionName)
        {
            options.AddPolicy(permissionName,
                builder => { builder.Requirements.Add(new PermissionRequirement(permissionName)); });
        }
    }
}