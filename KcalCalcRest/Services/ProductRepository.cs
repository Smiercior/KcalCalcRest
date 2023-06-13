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
}