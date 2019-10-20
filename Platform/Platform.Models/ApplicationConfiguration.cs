using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Platform.Models
{
	public static class ApplicationConfiguration
	{
		static ApplicationConfiguration()
		{
			Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

			var environmentConnectionString = Environment.GetEnvironmentVariable("CONN_STRING");
			ConnectionString = !string.IsNullOrEmpty(environmentConnectionString)
				? environmentConnectionString
				: Database["ConnectionString"];
		}
		
		public static IConfigurationRoot Configuration { get; private set; }

		public static IConfigurationSection Database => Configuration.GetSection("Database");
		
		public static ILogger Logger { get; set; }

		public static string ConnectionString { get; private set; }
	}
}