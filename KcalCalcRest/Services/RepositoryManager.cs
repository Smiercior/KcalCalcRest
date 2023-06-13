using AutoMapper;
using Microsoft.AspNetCore.Identity;
using KcalCalcRest.Models;
using KcalCalcRest.Data;
using KcalCalcRest.Interfaces;

namespace KcalCalcRest.Services;

public class RepositoryManager : IRepositoryManager {
	private ApplicationDbContext _appDbContext;

	private IUserAuthenticationRepository _userAuthenticationRepository;
	private UserManager<User> _userManager;
	private IMapper _mapper;

	public RepositoryManager(ApplicationDbContext appDbContext, UserManager<User> userManager, IMapper mapper) {
		_appDbContext = appDbContext;
		_userManager = userManager;    
		_mapper = mapper;
	}

	public IUserAuthenticationRepository UserAuthentication {
		get {
			if (_userAuthenticationRepository is null)
				_userAuthenticationRepository = new UserAuthenticationRepository(_userManager, _mapper);
			return _userAuthenticationRepository;
		}
	}
	public Task SaveAsync() => _appDbContext.SaveChangesAsync();
}