namespace KcalCalcRest.DTOs; 

public class UserProfileDTO {
	public string? Email { get; init; }
	public string? FirstName { get; init; }
	public string? LastName { get; init; }
	public DateTime JoinedAt { get; set; }
	
	public float? Weight { get; init; }
	public float? Height { get; init; }
}