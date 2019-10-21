using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Platform.Database;
using Platform.Models;
using Platform.Web.Services;
using Platform.Web.Services.SwaggerServices;

namespace Platform.Web
{
	public static class StartupExtensions
	{
		private static ILogger Logger => ApplicationConfiguration.Logger;

		public static void ConfigureLogger(this IServiceCollection services)
		{
			var loggerFactory = LoggerFactory.Create(builder =>
			{
				builder
					.AddFilter("Microsoft", LogLevel.Warning)
					.AddFilter("System", LogLevel.Warning)
					.AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
					.AddConsole()
					.AddEventLog();
			});
			ILogger logger = loggerFactory.CreateLogger<Program>();
			logger.LogInformation("Logger has been configured.");
			ApplicationConfiguration.Logger = logger;
		}

		public static void AddJwtAuthentication(this IServiceCollection services)
		{
			Logger.LogInformation("Configuring JwtBearer...");
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidIssuer = JwtOptions.Issuer,
					ValidateAudience = true,
					ValidAudience = JwtOptions.Audience,
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
			Logger.LogInformation("Configuring Services...");

			services.AddTransient<ApplicationDbContext>();
			services.AddSingleton<IRepository<User>, BaseRepository<User>>();

			services.AddSingleton<PasswordService>();
			services.AddSingleton<TokenService>();
		}

		public static void RegisterSwagger(this IServiceCollection services)
		{
			Logger.LogInformation("Configuing SwaggerGen...");
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(Startup.SwaggerConfigurationName, new OpenApiInfo()
				{
					Title = "Platform Swagger API",
					Version = Startup.SwaggerConfigurationName
				});
				c.OperationFilter<PlatformSwaggerOperationFilter>();
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					In = ParameterLocation.Header, Description = "Please insert JWT with Bearer into field",
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
			});
		}
	}
}