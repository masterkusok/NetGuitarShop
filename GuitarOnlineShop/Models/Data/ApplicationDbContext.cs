using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GuitarOnlineShop.Models.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Product> Products { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Product>().Property(prod => prod.XmlSpecs).HasColumnType("xml");
        }
	}
}
