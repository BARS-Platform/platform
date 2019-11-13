using Microsoft.EntityFrameworkCore;
using Platform.Fodels;
using Platform.Fodels.Interfaces;
using Platform.Fodels.Models;

namespace Platform.Migrations
{
	public class MigrationsDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(new ApplicationConfiguration().ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasKey(u => ((IPlatformModel)u).Id);
			base.OnModelCreating(modelBuilder);
		}
	}
}