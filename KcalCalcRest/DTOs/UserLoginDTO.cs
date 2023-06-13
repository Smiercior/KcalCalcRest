using System.ComponentModel.DataAnnotations;

namespace KcalCalcRest.DTOs; 

public class UserLoginDTO {
	[Required(ErrorMessage = "Email is required")]
	public string? Email { get; init; }
	
	[Required(ErrorMessage = "Password is required")]
	public string? Password { get; init; }
}