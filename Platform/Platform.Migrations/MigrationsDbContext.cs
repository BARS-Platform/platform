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
		
		public DbSet<RolePermission> RolePermissions { get; set; }

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
			modelBuilder.Entity<RolePermission>()
				.ToTable("RolePermissions");

			base.OnModelCreating(modelBuilder);
		}
	}
}
