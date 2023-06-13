using KcalCalcRest.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KcalCalcRest.Services; 

public abstract class RepositoryBase<T> where T : class {
	protected ApplicationDbContext AppDbContext;
	public RepositoryBase(ApplicationDbContext appDbContext) =>
		AppDbContext = appDbContext;

	public async Task<IQueryable<T>> FindAllAsync(bool trackChanges) =>
		!trackChanges ? await Task.Run(() => AppDbContext.Set<T>().AsNoTracking()) : await Task.Run(() => AppDbContext.Set<T>());

	public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) =>
		!trackChanges ? await Task.Run(() => AppDbContext.Set<T>().Where(expression).AsNoTracking()) : await Task.Run(() => AppDbContext.Set<T>().Where(expression));

	public async Task CreateAsync(T entity) => await Task.Run(() => AppDbContext.Set<T>().Add(entity));

	public async Task UpdateAsync(T entity) => await Task.Run(() => AppDbContext.Set<T>().Update(entity));
	public async Task RemoveAsync(T entity) => await Task.Run(() => AppDbContext.Set<T>().Remove(entity));
}