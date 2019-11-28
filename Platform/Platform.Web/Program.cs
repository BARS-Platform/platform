using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Platform.Migrations;

namespace Platform.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			ExecuteNewMigrations();
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
		
		private static void ExecuteNewMigrations()
		{
			using var context = new MigrationsDbContext();
			context.Database.Migrate();
		}
	}
}