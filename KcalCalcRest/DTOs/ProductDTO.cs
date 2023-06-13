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