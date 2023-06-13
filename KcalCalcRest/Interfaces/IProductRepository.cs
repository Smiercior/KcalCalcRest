using KcalCalcRest.Models;

namespace KcalCalcRest.Interfaces; 

public interface IProductRepository {
	Task<IEnumerable<Product>> GetAllProducts(bool trackChanges);
	Task CreateProduct(Product product);
	Task<Product?> GetProduct(int productId, bool trackChanges);
}