﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KcalCalcRest.Models {
	public class ProductEntries {
		[Key]
		[Required]
		public int Id { get; set; }

		[Required]
		[Range(0, 1000)]
		public float Amount { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime EntryDate { get; set; }

		[ForeignKey("Users")]
		public int UserId { get; set; }
		public virtual Users User { get; set; }

		[ForeignKey("Products")]
		public int ProductId { get; set; }
		public virtual Products Product { get; set; }
	}
}


