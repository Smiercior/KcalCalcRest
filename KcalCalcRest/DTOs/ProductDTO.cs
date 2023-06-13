using System.ComponentModel.DataAnnotations;

namespace KcalCalcRest.DTOs; 

public class ProductDTO {
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Brand { get; set; }
	public float? Kcal { get; set; }
	public float? Protein { get; set; }
	public float? Carbohydrate { get; set; }
	public float? Fat { get; set; }
	public float? BasicAmountGram { get; set; }
}

public class ProductCreationAndUpdateDTO {
	[Required]
	public string? Name { get; set; }
	[Required]
	public string? Brand { get; set; }
	[Required]
	public float? Kcal { get; set; }
	[Required]
	public float? Protein { get; set; }
	[Required]
	public float? Carbohydrate { get; set; }
	[Required]
	public float? Fat { get; set; }
	[Required]
	public float? BasicAmountGram { get; set; }
}
