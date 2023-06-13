using Microsoft.EntityFrameworkCore;
using KcalCalcRest.Models;

namespace KcalCalcRest.Data;
public class ApplicationDbContext : DbContext {
	public DbSet<User> Users { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<ProductEntry> ProductEntries { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
	}

	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);

		builder.Entity<ProductEntry>()
			.HasOne(productEntry => productEntry.User)
			.WithMany(user => user.ProductEntries)
			.HasForeignKey(productEntry => productEntry.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.Entity<ProductEntry>()
			.HasOne(productEntry => productEntry.Product)
			.WithMany(product => product.ProductEntries)
			.HasForeignKey(productEntry => productEntry.ProductId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}