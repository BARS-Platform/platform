using Microsoft.EntityFrameworkCore;
using Platform.Models;

namespace Platform.Database
{
	public class ApplicationDbContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(ApplicationConfiguration.ConnectionString);
		}
	}
}