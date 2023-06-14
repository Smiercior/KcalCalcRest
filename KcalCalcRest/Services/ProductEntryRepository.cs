using KcalCalcRest.Data;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.EntityFrameworkCore;

namespace KcalCalcRest.Services; 

public class ProductEntryRepository : RepositoryBase<ProductEntry>, IProductEntryRepository {
	public ProductEntryRepository(ApplicationDbContext appDbContext) : base(appDbContext) { }

	public async Task<IQueryable<ProductEntry>> GetAllUserEntriesToday(string userId) => 
		await FindByConditionAsync(e => e.UserId == userId && e.EntryDate.Date == DateTime.UtcNow.Date, false);

	public async Task CreateProductEntry(ProductEntry productEntry, string userId) {
		productEntry.UserId = userId;
		productEntry.EntryDate = DateTime.UtcNow;
		await CreateAsync(productEntry);
	}

	public async Task<ProductEntry?> GetProductEntry(int productEntryId) {
		return await FindByConditionAsync(e => e.Id == productEntryId, false).Result.SingleOrDefaultAsync();
	}

	public async Task DeleteProductEntry(ProductEntry productEntry) {
		await RemoveAsync(productEntry);
	}

	public async Task<IQueryable<IGrouping<DateTime, ProductEntry>>> GetEntriesGroupedByDate(string userId) {
		var entries = await FindByConditionAsync(e => e.UserId == userId, false);
		return entries.GroupBy(e => e.EntryDate.Date);
	}
}