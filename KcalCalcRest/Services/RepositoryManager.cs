using AutoMapper;
using Microsoft.AspNetCore.Identity;
using KcalCalcRest.Models;
using KcalCalcRest.Data;
using KcalCalcRest.Interfaces;

namespace KcalCalcRest.Services;

public class RepositoryManager : IRepositoryManager {
	private ApplicationDbContext _appDbContext;

	private IUserAuthenticationRepository? _userAuthenticationRepository;
	private UserManager<User> _userManager;
	private IConfiguration _configuration;
	private IMapper _mapper;

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
	public Task SaveAsync() => _appDbContext.SaveChangesAsync();
}