using AutoMapper;
using KcalCalcRest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KcalCalcRest.Controllers; 

[Route("api/products")]
[ApiController]
[Authorize] // TODO: add test for this
public class ProductsController : BaseApiController {
	public ProductsController(IRepositoryManager repository, IMapper mapper) : base(repository, mapper) { }
	
	[HttpGet]
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