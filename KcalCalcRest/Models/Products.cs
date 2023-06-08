using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KcalCalcRest.Models {
	public class Products {
		[Key]
		[Required]
		public int Id { get; set; }

		[Required]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "Product name should be longer than 2 characters and shorter than 20 characters")]
		public string Name { get; set; }

		[Required]
		[StringLength(20, MinimumLength = 2, ErrorMessage = "Brand should be longer than 2 characters and shorter than 20 characters")]
		public string Brand { get; set; }

		[Required]
		[Range(0, 10000)]
		public float Kcal { get; set; }

		[Required]
		[Range(0, 1000)]
		public float Protein { get; set; }

		[Required]
		[Range(0, 1000)]
		public float Carbohydrate { get; set; }

		[Required]
		[Range(0, 1000)]
		public float Fat { get; set; }

		[Required]
		[Range(0, 1000)]
		public float BasicAmountGram { get; set; }

		public virtual ICollection<ProductEntries> ProductEntries { get; set; }
	}
}



