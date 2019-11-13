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
				.ToTable("users");
			modelBuilder.Entity<Role>()
				.ToTable("roles");
			modelBuilder.Entity<UserRole>()
				.ToTable("user_roles");
			modelBuilder.Entity<Permission>()
				.ToTable("permissions");

			MapEntityToNormalNames<User>(modelBuilder);
			MapEntityToNormalNames<Role>(modelBuilder);
			MapEntityToNormalNames<UserRole>(modelBuilder);
			MapEntityToNormalNames<Permission>(modelBuilder);
			
			modelBuilder.Entity<User>()
				.HasKey(b => b.Id)
				.HasName("pk_id");
			modelBuilder.Entity<Permission>()
				.HasKey(b => b.Id)
				.HasName("pk_permission_id");
			modelBuilder.Entity<Role>()
				.HasKey(b => b.Id)
				.HasName("pk_role_id");
			modelBuilder.Entity<Role>()
				.HasMany(r => r.Permissions)
				.WithOne();
			modelBuilder.Entity<UserRole>()
				.HasKey(b => b.Id)
				.HasName("pk_user_role_id");

			modelBuilder.Entity<User>().HasKey(u => ((IPlatformModel)u).Id);
			base.OnModelCreating(modelBuilder);
		}
	}
}
