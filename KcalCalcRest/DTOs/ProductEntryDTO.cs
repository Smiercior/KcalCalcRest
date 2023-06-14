using System.ComponentModel.DataAnnotations;

namespace KcalCalcRest.DTOs;

public class ProductEntryDTO {
	public int Id { get; set; }
	public float? Amount { get; set; }
	public DateTime? EntryDate { get; set; }
	
	public int? ProductId { get; set; }
	public string? Name { get; set; }
	public string? Brand { get; set; }
	public float? Kcal { get; set; }
	public float? Protein { get; set; }
	public float? Carbohydrate { get; set; }
	public float? Fat { get; set; }
}

public class ProductEntryCreationDTO {
	[Required]
	public float? Amount { get; set; }
	[Required]
	public int ProductId { get; set; }
}