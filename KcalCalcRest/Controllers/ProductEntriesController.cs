using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KcalCalcRest.Controllers;

[Route("api/")]
[ApiController]
public class ProductsEntriesController : BaseApiController {
	public ProductsEntriesController(IRepositoryManager repository, IMapper mapper) : base(repository, mapper) { }

	[Authorize(AuthenticationSchemes = "Bearer")]
	[HttpPost("product-entries")]
	public async Task<IActionResult> CreateProductEntry([FromBody] ProductEntryCreationDTO productEntryData) {
		var product = _mapper.Map<ProductEntry>(productEntryData);
		await _repository.Products.CreateProduct(product);
		await _repository.SaveAsync();
		var productDataToReturn = _mapper.Map<ProductDTO>(product);
		return CreatedAtRoute("ProductById",
			new {
				productId = productDataToReturn.Id
			},
			productDataToReturn);
	}
}
