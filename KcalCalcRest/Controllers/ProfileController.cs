using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace KcalCalcRest.Controllers; 

[Route("api/profile")]
[ApiController]
public class ProfileController : BaseApiController {
	private readonly IHttpContextAccessor _contextAccessor;
	
	public ProfileController(IRepositoryManager repository, IMapper mapper, IHttpContextAccessor contextAccessor) :
		base(repository, mapper) {
		_contextAccessor = contextAccessor;
	}
	
	[Authorize(AuthenticationSchemes = "Bearer")]
	[HttpGet]
	public async Task<IActionResult> GetProfile() {
		Console.WriteLine("User Id: " + 
		                  _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
		var username = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
		Console.WriteLine("Username: " + username);
		
		if (username is null) {
			return BadRequest("User not found");
		}
		
		var user = await _repository.UserAuthentication.GetUserAsync(username);
		var userDTO = _mapper.Map<UserProfileDTO>(user);
		return Ok(userDTO);
	}
}