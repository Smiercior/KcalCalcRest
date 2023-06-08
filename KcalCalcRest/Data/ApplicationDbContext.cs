using Microsoft.EntityFrameworkCore;
using KcalCalcRest.Models;

namespace KcalCalcRest.Data{
    public class ApplicationDbContext : DbContext{
		public DbSet<Users> Users { get; set; }
		public DbSet<Products> Products { get; set; }
		public DbSet<ProductEntries> ProductEntries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){
        }

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);

			builder.Entity<ProductEntries>()
				.HasOne(productEntry => productEntry.User)
				.WithMany(user => user.ProductEntries)
				.HasForeignKey(productEntry => productEntry.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<ProductEntries>()
				.HasOne(productEntry => productEntry.Product)
				.WithMany(product => product.ProductEntries)
				.HasForeignKey(productEntry => productEntry.ProductId)
				.OnDelete(DeleteBehavior.Cascade);
        }
    }
}