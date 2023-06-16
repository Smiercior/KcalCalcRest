using AutoMapper;
using Microsoft.AspNetCore.Identity;
using KcalCalcRest.Models;
using KcalCalcRest.Data;
using KcalCalcRest.Interfaces;

namespace KcalCalcRest.Services;

public class RepositoryManager : IRepositoryManager {
	private readonly ApplicationDbContext _appDbContext;

	private readonly UserManager<User> _userManager;
	private readonly IConfiguration _configuration;
	private readonly IMapper _mapper;
	
	private IUserAuthenticationRepository? _userAuthenticationRepository;
	private IProductRepository? _productRepository;
	private IProductEntryRepository? _productEntryRepository;
	
	public RepositoryManager(ApplicationDbContext appDbContext, UserManager<User> userManager,  IConfiguration configuration, IMapper mapper) {
		_appDbContext = appDbContext;
		_userManager = userManager;
		_configuration = configuration;
		_mapper = mapper;
	}

	public IUserAuthenticationRepository UserAuthentication {
		get {
			_userAuthenticationRepository ??= new UserAuthenticationRepository(_userManager, _configuration, _mapper);
			return _userAuthenticationRepository;
		}
	}
	
	public IProductRepository Products {
		get {
			_productRepository ??= new ProductRepository(_appDbContext);
			return _productRepository;
		}
	}
	
	public IProductEntryRepository ProductEntries {
		get {
			_productEntryRepository ??= new ProductEntryRepository(_appDbContext);
			return _productEntryRepository;
		}
	}
	
	public Task SaveAsync() => _appDbContext.SaveChangesAsync();
}