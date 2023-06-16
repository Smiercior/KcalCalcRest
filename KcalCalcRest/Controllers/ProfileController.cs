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
	public ProfileController(IRepositoryManager repository, IMapper mapper) : base(repository, mapper) { }
	
	[Authorize(AuthenticationSchemes = "Bearer")]
	[HttpGet]
	public async Task<IActionResult> GetProfile() {
		var user = GetCurrentUser();
		if (user is null) {
			return BadRequest($"User not found.");
		}
		
		var userDTO = _mapper.Map<UserProfileDTO>(user);
		
		return Ok(userDTO);
	}
}