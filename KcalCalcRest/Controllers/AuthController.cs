using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KcalCalcRest.Controllers; 

[Route("api/auth")]
[ApiController]
public class AuthController : BaseApiController {
	private readonly SignInManager<User> _signInManager;
	public AuthController(SignInManager<User> signInManager, IRepositoryManager repository, IMapper mapper) : base(repository, mapper) {
		_signInManager = signInManager;
	}
	
	[AllowAnonymous]
	[HttpPost("register")]
	public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userRegistration) {
		var userResult = await _repository.UserAuthentication.RegisterUserAsync(userRegistration);
		return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
	}

	[AllowAnonymous]
	[HttpPost("login")]
	public async Task<IActionResult> Authenticate([FromBody] UserLoginDTO userLogin) {
		if (!ModelState.IsValid) {
			return BadRequest(ModelState);
		}
		
		var result = await _signInManager.PasswordSignInAsync(userLogin.Email!, userLogin.Password!, isPersistent: false, lockoutOnFailure: true);
		if (result.Succeeded) {
			return Ok(new { Token = await _repository.UserAuthentication.CreateTokenAsync(userLogin.Email!) });
		}

		return Unauthorized(result.IsLockedOut ? "The account is locked out." : "Invalid login attempt.");
	}
}