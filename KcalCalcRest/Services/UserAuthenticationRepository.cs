using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Identity;

namespace KcalCalcRest.Services; 

internal sealed class UserAuthenticationRepository : IUserAuthenticationRepository {
	private readonly UserManager<User> _userManager;
	private readonly IMapper _mapper;
	
	public UserAuthenticationRepository(UserManager<User> userManager, IMapper mapper) {
		_userManager = userManager;
		_mapper = mapper;
	}
	
	public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDTO userRegistration) {
		var user = _mapper.Map<User>(userRegistration);
		
		// Check if userRegistration.Password == userRegistration.Password2.
		if (userRegistration.Password != userRegistration.Password2) {
			return IdentityResult.Failed(new IdentityError {
				Code = "PasswordMismatch",
				Description = "Passwords do not match"
			});
		}

		var result = await _userManager.CreateAsync(user, userRegistration.Password);
		return result;
	}
}