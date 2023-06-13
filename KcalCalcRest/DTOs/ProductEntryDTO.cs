using System.ComponentModel.DataAnnotations;

namespace KcalCalcRest.DTOs;

public class ProductEntryDTO {
	public string? Id { get; set; }
	public ProductDTO? Product { get; set; }
	public float? Amount { get; set; }
	public DateTime? EntryDate { get; set; }
}

public class ProductEntryCreationDTO {
	[Required]
	public float? Amount { get; set; }
}