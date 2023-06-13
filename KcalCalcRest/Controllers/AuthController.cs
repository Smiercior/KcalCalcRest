using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KcalCalcRest.Controllers; 

[Route("api/register")]
[ApiController]
public class AuthController : BaseApiController {
	public AuthController(IRepositoryManager repository, IMapper mapper) : base(repository, mapper) { }

	[HttpPost]
	public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userRegistration) {
		var userResult = await _repository.UserAuthentication.RegisterUserAsync(userRegistration);
		return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
	}
}