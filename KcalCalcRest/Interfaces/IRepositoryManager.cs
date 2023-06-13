namespace KcalCalcRest.Interfaces; 

public interface IRepositoryManager {
	IUserAuthenticationRepository UserAuthentication { get; }
	IProductRepository Products { get; }
	IProductEntryRepository ProductEntries { get; }
	Task SaveAsync();
}
