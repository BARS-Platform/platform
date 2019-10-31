using Microsoft.EntityFrameworkCore;
using Platform.Models;

namespace Platform.Database
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(ApplicationConfiguration.ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.ToTable("users");

			MapEntityToNormalNames<User>(modelBuilder);
			
			modelBuilder.Entity<User>()
				.HasKey(b => b.Id)
				.HasName("pk_id");

			base.OnModelCreating(modelBuilder);
		}

		private void MapEntityToNormalNames<T>(ModelBuilder modelBuilder) where T : class
		{
			foreach (var property in typeof(T).GetProperties())
			{
				modelBuilder.Entity<T>().Property(property.Name).HasColumnName(property.Name.ToLower());
			}
		}
	}
}