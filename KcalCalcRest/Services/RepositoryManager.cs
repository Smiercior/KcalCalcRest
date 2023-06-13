using AutoMapper;
using Microsoft.AspNetCore.Identity;
using KcalCalcRest.Models;
using KcalCalcRest.Data;
using KcalCalcRest.Interfaces;

namespace KcalCalcRest.Services;

public class RepositoryManager : IRepositoryManager {
	private ApplicationDbContext _appDbContext;

	private UserManager<User> _userManager;
	private IConfiguration _configuration;
	private IMapper _mapper;
	
	private IUserAuthenticationRepository? _userAuthenticationRepository;
	private IProductRepository _productRepository;
	private IProductEntryRepository _productEntryRepository;
	
	public RepositoryManager(ApplicationDbContext appDbContext, UserManager<User> userManager,  IConfiguration configuration, IMapper mapper) {
		_appDbContext = appDbContext;
		_userManager = userManager;
		_configuration = configuration;
		_mapper = mapper;
	}

	public IUserAuthenticationRepository UserAuthentication {
		get {
			if (_userAuthenticationRepository is null) {
				_userAuthenticationRepository = new UserAuthenticationRepository(_userManager, _configuration, _mapper);
			}
			return _userAuthenticationRepository;
		}
	}
	
	public IProductRepository Products {
		get {
			if (_productRepository is null){
				_productRepository = new ProductRepository(_appDbContext);
			}
			return _productRepository;
		}
	}
	
	public IProductEntryRepository ProductEntries {
		get {
			if (_productEntryRepository is null){
				_productEntryRepository = new ProductEntryRepository(_appDbContext);
			}
			return _productEntryRepository;
		}
	}
	
	public Task SaveAsync() => _appDbContext.SaveChangesAsync();
}