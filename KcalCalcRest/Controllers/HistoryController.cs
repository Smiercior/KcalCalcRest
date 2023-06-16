using AutoMapper;
using KcalCalcRest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KcalCalcRest.Controllers; 

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/")]
[ApiController]
public class HistoryController : BaseApiController {
	public HistoryController(IRepositoryManager repository, IMapper mapper) : base(repository, mapper) { }
	
	[HttpGet("history-per-day")]
	public async Task<IActionResult> GetEntriesPerDay() {
		var user = await GetCurrentUser();
		if (user is null) {
			return BadRequest("User not found.");
		}
		
		var productEntriesByDate = await _repository.ProductEntries.GetEntriesGroupedByDate(user.Id);
		var toReturn = productEntriesByDate.Select(g => new {
			Date = g.Key.ToString("yyyy-MM-dd"),
			Kcal = g.Sum(e => e.Product.Kcal),
			Protein = g.Sum(e => e.Product.Protein),
			Carbohydrate = g.Sum(e => e.Product.Carbohydrate),
			Fat = g.Sum(e => e.Product.Fat),
		});
		return Ok(toReturn);
	}
	

	[HttpGet("daily-summary")]
	public async Task<IActionResult> GetDailySummary() {
		var user = await GetCurrentUser();
		if (user is null) {
			return BadRequest("User not found.");
		}
		
		var productEntriesToday = await _repository.ProductEntries.GetAllUserEntriesToday(user.Id);
		var toReturn = new {
			Kcal = productEntriesToday.Sum(e => e.Product.Kcal),
			Protein = productEntriesToday.Sum(e => e.Product.Protein),
			Carbohydrate = productEntriesToday.Sum(e => e.Product.Carbohydrate),
			Fat = productEntriesToday.Sum(e => e.Product.Fat),
			user.KcalLimit,
			user.ProteinLimit,
			user.CarbohydrateLimit,
			user.FatLimit
		};
		return Ok(toReturn);
	}
}