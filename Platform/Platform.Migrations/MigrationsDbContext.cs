using Microsoft.EntityFrameworkCore;
using Platform.Fodels;
using Platform.Fodels.Interfaces;
using Platform.Fodels.Models;

namespace Platform.Migrations
{
	public class MigrationsDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		
		public DbSet<Role> Roles { get; set; }
		
		public DbSet<UserRole> UserRoles { get; set; }
		
		public DbSet<Permission> Permissions { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(new ApplicationConfiguration().ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.ToTable("Users");
			modelBuilder.Entity<Role>()
				.ToTable("Roles");
			modelBuilder.Entity<UserRole>()
				.ToTable("UserRoles");
			modelBuilder.Entity<Permission>()
				.ToTable("Permissions");

			modelBuilder.Entity<User>().HasKey(u => ((IPlatformModel)u).Id);
			modelBuilder.Entity<Permission>().HasKey(u => ((IPlatformModel)u).Id);
			modelBuilder.Entity<Role>().HasKey(u => ((IPlatformModel)u).Id);
			modelBuilder.Entity<UserRole>().HasKey(u => ((IPlatformModel)u).Id);

			base.OnModelCreating(modelBuilder);
		}
	}
}
