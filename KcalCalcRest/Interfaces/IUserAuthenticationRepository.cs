using KcalCalcRest.Models;
using Microsoft.AspNetCore.Identity;

namespace KcalCalcRest.Interfaces; 

public interface IUserAuthenticationRepository {
	Task<IdentityResult> RegisterUserAsync(User user);
}