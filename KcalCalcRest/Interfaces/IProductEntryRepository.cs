using KcalCalcRest.Models;

namespace KcalCalcRest.Interfaces; 

public interface IProductEntryRepository {
	Task<IQueryable<ProductEntry>> GetAllUserEntriesToday(string userId);
	Task CreateProductEntry(ProductEntry productEntry, string userId);
	Task<ProductEntry?> GetProductEntry(int productEntryId);
	Task DeleteProductEntry(ProductEntry productEntry);
	Task<IQueryable<IGrouping<DateTime, ProductEntry>>> GetEntriesGroupedByDate(string userId);
}