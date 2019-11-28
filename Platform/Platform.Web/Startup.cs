using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VueCliMiddleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Platform.Migrations;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Platform.Web
{
	public class Startup
	{
		public static readonly string SwaggerConfigurationName = "v1";

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddJwtAuthentication();
			services.AddPoliciesAuthorization();

			services.RegisterServices();

			services.AddSpaStaticFiles(opt => opt.RootPath = "ClientApp/dist");

			services.RegisterSwagger();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IServiceProvider provider)
		{
			provider.CheckRegisteredRolesForExisting();
			provider.CheckRegisteredPermissionsForExisting();
			provider.CheckRegisteredRolePermissionsForExisting();
			
			app.UseHttpsRedirection();

			if (env.IsDevelopment())
			{
				logger.LogInformation("Using developers exception handling");
				app.UseDeveloperExceptionPage();
			}
			else
			{
				logger.LogInformation("Using production exception handling");
				app.UseExceptionHandler("/System/Error");
				app.UseHsts();
			}

			logger.LogInformation("Initializing Swagger...");
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{SwaggerConfigurationName}/swagger.json", "Platform API");
                options.DocExpansion(DocExpansion.None);
            });

            app.UseRouting();

			app.UseAuthentication();
            app.UseAuthorization();

			app.UseSpaStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

				endpoints.MapToVueCliProxy(
					"{*path}",
					new SpaOptions {SourcePath = "ClientApp"}
				);
			});
		}
	}
}