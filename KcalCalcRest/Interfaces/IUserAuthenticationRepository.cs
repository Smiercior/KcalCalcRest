using KcalCalcRest.DTOs;
using Microsoft.AspNetCore.Identity;

namespace KcalCalcRest.Interfaces; 

public interface IUserAuthenticationRepository {
	Task<IdentityResult> RegisterUserAsync(UserRegistrationDTO userRegistration);
}