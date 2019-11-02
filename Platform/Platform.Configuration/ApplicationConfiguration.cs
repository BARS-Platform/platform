using System;
using Microsoft.Extensions.Configuration;

namespace Platform.Configuration
{
	public class ApplicationConfiguration
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

		public static string ConnectionString { get; private set; }
	}
}