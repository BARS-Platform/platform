using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VueCliMiddleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Platform.Database;
using Platform.Models;
using Platform.Web.Services.SwaggerServices;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Platform.Web
{
	public class Startup
	{
		public static readonly string SwaggerConfigurationName = "v1";

		public void ConfigureServices(IServiceCollection services)
		{
			services.ConfigureLogger();
			
			services.AddControllers();

			services.AddJwtAuthentication();

			services.RegisterServices();

			services.AddSpaStaticFiles(opt => opt.RootPath = "ClientApp/dist");

			services.RegisterSwagger();

            services.AddSwaggerGen(c =>
			{
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Platform Swagger API",
                    Version = "v1"
                });

                c.OperationFilter<PlatformSwaggerOperationFilter>();

            });
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			using (var context = new ApplicationDbContext())
			{
				context.Database.Migrate();
			}

			app.UseRouting();

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
					new SpaOptions {SourcePath = "ClientApp"}
				);
			});
		}
	}
}