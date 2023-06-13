using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KcalCalcRest.Controllers;

[Route("api/")]
[ApiController]
public class ProductsEntriesController : BaseApiController {
	private IHttpContextAccessor _contextAccessor;
	
	public ProductsEntriesController(IRepositoryManager repository, IMapper mapper, IHttpContextAccessor contextAccessor) : base(repository, mapper) {
		_contextAccessor = contextAccessor;
	}

	[Authorize(AuthenticationSchemes = "Bearer")]
	[HttpPost("product-entries")]
	public async Task<IActionResult> CreateProductEntry([FromBody] ProductEntryCreationDTO productEntryData) {
		var productEntry = _mapper.Map<ProductEntry>(productEntryData);
		
		var username = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
		if (username is null) {
			return BadRequest("Couldn't get username from context.");
		}
		var user = await _repository.UserAuthentication.GetUserAsync(username);
		if (user is null) {
			return BadRequest("User not found.");
		}
		
		await _repository.ProductEntries.CreateProductEntry(productEntry, user.Id);
		await _repository.SaveAsync();
		var productEntryDataToReturn = _mapper.Map<ProductEntryDTO>(productEntry);
		return CreatedAtRoute("ProductEntryById",
			new {
				productEntryId = productEntryDataToReturn.Id
			},
			productEntryDataToReturn);
	}
	
	[Authorize(AuthenticationSchemes = "Bearer")]
	[HttpGet("product-entries/{productEntryId:int}", Name = "ProductEntryById")]
	public async Task<IActionResult> GetProductEntry(int productEntryId)
	{
		var productEntry = await _repository.ProductEntries.GetProductEntry(productEntryId);
		if (productEntry is null) {
			return NotFound();
		}
		
		var productEntryDTO = _mapper.Map<ProductEntryDTO>(productEntry);
		return Ok(productEntryDTO);
}