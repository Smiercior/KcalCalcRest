using KcalCalcRest.DTOs;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Identity;

namespace KcalCalcRest.Interfaces; 

public interface IUserAuthenticationRepository {
	Task<IdentityResult> RegisterUserAsync(UserRegistrationDTO userRegistration);
	Task<bool> ValidateUserAsync(UserLoginDTO userLogin);
	Task<string> CreateTokenAsync();
	Task<User> GetUserAsync(string email);
	Task<User> GetCurrentUserAsync();
}