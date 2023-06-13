using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KcalCalcRest.Controllers; 

[Route("api/products")]
[ApiController]
public class ProductsController : BaseApiController {
	public ProductsController(IRepositoryManager repository, IMapper mapper) : base(repository, mapper) { }
	
	[HttpPost]
	[Authorize]
	public async Task<IActionResult> CreateTeacher([FromBody] ProductDTO teacher) {
		var product = _mapper.Map<Product>(teacher);
		await _repository.Products.CreateProduct(product);
		await _repository.SaveAsync();
		var productDTO = _mapper.Map<ProductDTO>(product);
		return CreatedAtRoute("TeacherById",
			new {
				productId = productDTO.Id
			},
			productDTO);
	}


	[HttpGet("{productId}", Name = "ProductById")]
	public async Task<IActionResult> GetTeacher(int productId)
	{
		var teacher = await _repository.Products.GetProduct(productId, trackChanges: false);
		if (teacher is null) {
			// TODO: _logger.LogInfo($"Product with id: {productId} doesn't exist in the database.");
			return NotFound();
		}
		else {
			var productDTO = _mapper.Map<ProductDTO>(teacher);
			return Ok(productDTO);
		}
	}
	
	[HttpGet]
	// [Authorize] // TODO: add test for this
	public async Task<IActionResult> GetProducts() {
		try {
			var products = await _repository.Products.GetAllProducts(trackChanges: false);
			var teachersDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
			return Ok(teachersDto);
		}
		catch (Exception e) {
			// TODO: _logger.LogError($"Something went wrong in the {nameof(GetProducts)} action {e}");
			return StatusCode(500, "Internal server error");
		}
	}
}