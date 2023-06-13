using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KcalCalcRest.Controllers; 

[Route("api/auth")]
[ApiController]
public class AuthController : BaseApiController {
	public AuthController(IRepositoryManager repository, IMapper mapper) : base(repository, mapper) { }
	
	[AllowAnonymous]
	[HttpPost("register")]
	public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userRegistration) {
		var userResult = await _repository.UserAuthentication.RegisterUserAsync(userRegistration);
		return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
	}

	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<IActionResult> Authenticate([FromBody] UserLoginDTO userLogin) {
		return !await _repository.UserAuthentication.ValidateUserAsync(userLogin) 
			? Unauthorized() 
			: Ok(new { Token = await _repository.UserAuthentication.CreateTokenAsync() });
	}
}