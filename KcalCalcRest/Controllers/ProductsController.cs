using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KcalCalcRest.Controllers; 

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/products/")]
[ApiController]
public class ProductsController : BaseApiController {
	public ProductsController(IRepositoryManager repository, IMapper mapper) : base(repository, mapper) { }
	
	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromBody] ProductCreationAndUpdateDTO productData) {
		var product = _mapper.Map<Product>(productData);
		await _repository.Products.CreateProduct(product);
		await _repository.SaveAsync();
		var productDataToReturn = _mapper.Map<ProductDTO>(product);
		return CreatedAtRoute("ProductById",
			new {
				productId = productDataToReturn.Id
			},
			productDataToReturn);
	}

	[HttpGet("{productId}", Name = "ProductById")]
	public async Task<IActionResult> GetProduct(int productId)
	{
		var product = await _repository.Products.GetProduct(productId, trackChanges: false);
		if (product is null) {
			return NotFound();
		}

		var productDTO = _mapper.Map<ProductDTO>(product);
		return Ok(productDTO);
	}
	
	[HttpGet]
	public async Task<IActionResult> GetProducts() {
		try {
			var products = await _repository.Products.GetAllProducts(trackChanges: false);
			var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);
			return Ok(productsDTO);
		}
		catch (Exception) {
			return StatusCode(500, "Internal server error");
		}
	}
}