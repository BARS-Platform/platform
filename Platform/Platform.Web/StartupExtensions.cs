using System;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Platform.Domain.Common;
using Platform.Domain.DomainServices;
using Platform.Domain.Services;
using Platform.Fatabase;
using Platform.Fodels;
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
	}
}