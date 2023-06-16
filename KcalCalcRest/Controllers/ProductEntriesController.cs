using AutoMapper;
using AutoMapper.QueryableExtensions;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KcalCalcRest.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/product-entries/")]
[ApiController]
public class ProductsEntriesController : BaseApiController {
	public ProductsEntriesController(IRepositoryManager repository, IMapper mapper) : base(repository, mapper) { }

	[HttpPost]
	public async Task<IActionResult> CreateProductEntry([FromBody] ProductEntryCreationDTO productEntryData) {
		var productEntry = _mapper.Map<ProductEntry>(productEntryData);
		
		var user = await GetCurrentUser();
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

	[HttpGet("{productEntryId:int}", Name = "ProductEntryById")]
	public async Task<IActionResult> GetProductEntry(int productEntryId) {
		var productEntry = await _repository.ProductEntries.GetProductEntry(productEntryId);
		if (productEntry is null) {
			return NotFound();
		}
	
		var productEntryDTO = _mapper.Map<ProductEntryDTO>(productEntry);
		return Ok(productEntryDTO);
	}
	
	[HttpDelete("{productEntryId:int}")]
	public async Task<IActionResult> DeleteProductEntry(int productEntryId) {
		var productEntry = await _repository.ProductEntries.GetProductEntry(productEntryId);
		if (productEntry is null) {
			return NotFound();
		}
		
		await _repository.ProductEntries.DeleteProductEntry(productEntry);
		await _repository.SaveAsync();
		return NoContent();
	}
	
	[HttpGet("today")]
	public async Task<IActionResult> GetUserEntriesFromToday() {
		var user = await GetCurrentUser();
		if (user is null) {
			return BadRequest("User not found.");
		}
		
		var productEntriesDTO = (await _repository.ProductEntries.GetAllUserEntriesToday(user.Id))
			.ProjectTo<ProductEntryDTO>(_mapper.ConfigurationProvider)
			.ToList();
		return Ok(productEntriesDTO);
	}
}
