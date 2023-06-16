using KcalCalcRest.DTOs;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Identity;

namespace KcalCalcRest.Interfaces; 

public interface IUserAuthenticationRepository {
	Task<IdentityResult> RegisterUserAsync(UserRegistrationDTO userRegistration);
	Task<string?> CreateTokenAsync(string username);
	Task<User?> GetUserAsync(string email);
}