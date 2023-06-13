using KcalCalcRest.Data;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.EntityFrameworkCore;

namespace KcalCalcRest.Services; 

public class ProductRepository : RepositoryBase<Product>, IProductRepository {
	public ProductRepository(ApplicationDbContext appDbContext) : base(appDbContext) { }

	public async Task<IEnumerable<Product>> GetAllProducts(bool trackChanges)
		=> await FindAllAsync(trackChanges).Result.OrderBy(p => p.Name)
			.ToListAsync();

	public async Task CreateProduct(Product product) => await CreateAsync(product);
	public async Task<Product?> GetProduct(int productId, bool trackChanges)
		=> await FindByConditionAsync(c => c.Id.Equals(productId), trackChanges).Result
			.SingleOrDefaultAsync();
}