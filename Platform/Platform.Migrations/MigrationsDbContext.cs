using Microsoft.EntityFrameworkCore;
using Platform.Fodels;
using Platform.Fodels.Interfaces;
using Platform.Fodels.Models;
using Platform.Fodels.Models.Address;

namespace Platform.Migrations
{
	public class MigrationsDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Apartment> Apartments { get; set; }
		public DbSet<House> Houses { get; set; }
		public DbSet<Street> Streets { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<State> States { get; set; }
		public DbSet<Country> Countries { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(new ApplicationConfiguration().ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region MapPrimaryKey

			modelBuilder.Entity<User>().HasKey(u => ((IEntityBase) u).Id);
			modelBuilder.Entity<Apartment>().HasKey(u => ((IEntityBase) u).Id);
			modelBuilder.Entity<House>().HasKey(u => ((IEntityBase) u).Id);
			modelBuilder.Entity<Street>().HasKey(u => ((IEntityBase) u).Id);
			modelBuilder.Entity<City>().HasKey(u => ((IEntityBase) u).Id);
			modelBuilder.Entity<State>().HasKey(u => ((IEntityBase) u).Id);
			modelBuilder.Entity<Country>().HasKey(u => ((IEntityBase) u).Id);

			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}