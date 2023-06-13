using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KcalCalcRest.Controllers; 

[Route("api/")]
[ApiController]
public class HistoryController : BaseApiController {
	private IHttpContextAccessor _contextAccessor;

	public HistoryController(IRepositoryManager repository, IMapper mapper, IHttpContextAccessor contextAccessor) : base(repository, mapper) {
		_contextAccessor = contextAccessor;
	}
	
	[Authorize(AuthenticationSchemes = "Bearer")]
	[HttpGet("history-per-day")]
	public async Task<IActionResult> GetUserEntriesFromToday() {
		var username = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
		if (username is null) {
			return BadRequest("Couldn't get username from context.");
		}
		var user = await _repository.UserAuthentication.GetUserAsync(username);
		if (user is null) {
			return BadRequest("User not found.");
		}
		
		var productEntries = await _repository.ProductEntries.Get(user.Id);
	}
}