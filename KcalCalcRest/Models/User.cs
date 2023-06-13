using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KcalCalcRest.Models;

public class User : IdentityUser {

	[StringLength(40, MinimumLength = 2, ErrorMessage = "FirstName should be longer than 2 characters and shorter than 40 characters")]
	public string? FirstName { get; set; }

	[StringLength(40, MinimumLength = 2, ErrorMessage = "LastName should be longer than 2 characters and shorter than 40 characters")]
	public string? LastName { get; set; }

	[DataType(DataType.DateTime)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	public DateTime JoinedAt { get; set; }
	
	public float? Weight { get; set; }
	public float? Height { get; set; }
	
	public float? KcalLimit { get; set; }
	public float? ProteinLimit { get; set; }
	public float? CarbohydrateLimit { get; set; }
	public float? FatLimit { get; set; }

	public virtual ICollection<ProductEntry> ProductEntries { get; set; }
}


