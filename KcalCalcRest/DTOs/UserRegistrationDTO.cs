using System.ComponentModel.DataAnnotations;

namespace KcalCalcRest.DTOs;

public class UserRegistrationDTO {
	[Required]
	public string? Email { get; init; }
	[Required]
	public string? Password { get; init; }
	[Required]
	public string? Password2 { get; init; } // TODO: use this
	
	public float? Weight { get; init; }
	public float? Height { get; init; }
}