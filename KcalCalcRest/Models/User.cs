using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KcalCalcRest.Models;
public class User : IdentityUser {
	[Required]
	[EmailAddress]
	public string Email { get; set; }

	[StringLength(40, MinimumLength = 2, ErrorMessage = "FirstName should be longer than 2 characters and shorter than 40 characters")]
	public string FirstName { get; set; }

	[StringLength(40, MinimumLength = 2, ErrorMessage = "LastName should be longer than 2 characters and shorter than 40 characters")]
	public string LastName { get; set; }

	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
	public DateTime JoinedAt { get; set; }

	public virtual ICollection<ProductEntry> ProductEntries { get; set; }
}


