using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VueCliMiddleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Platform.Web
{
	public class Startup
	{
		public static readonly string SwaggerConfigurationName = "v1";

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddControllers();

			AddJwtAuthentication(services);

			services.AddSpaStaticFiles(opt => opt.RootPath = "ClientApp/dist");

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(SwaggerConfigurationName, new OpenApiInfo()
				{
					Title = "Platform Swagger API",
					Version = SwaggerConfigurationName
				});
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

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			// global cors policy
			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint($"/swagger/{SwaggerConfigurationName}/swagger.json", "Platform API");
				options.DocExpansion(DocExpansion.None);
			});

			app.UseSpaStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

				endpoints.MapToVueCliProxy(
					"{*path}",
					new SpaOptions {SourcePath = "ClientApp"},
					(System.Diagnostics.Debugger.IsAttached) ? "serve" : null
				);
			});
		}

		public void AddJwtAuthentication(IServiceCollection services)
		{
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
	}
}