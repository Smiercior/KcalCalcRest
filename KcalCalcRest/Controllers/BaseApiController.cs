using AutoMapper;
using KcalCalcRest.Interfaces;
using KcalCalcRest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KcalCalcRest.Controllers; 

[Authorize(AuthenticationSchemes = "Bearer")]
public class BaseApiController : ControllerBase {
	protected readonly IRepositoryManager _repository;
	protected readonly IMapper _mapper;

	public BaseApiController(IRepositoryManager repository, IMapper mapper) {
		_repository = repository;
		_mapper = mapper;
	}
	
	protected async Task<User?> GetCurrentUser() {
		var username = User.FindFirstValue(ClaimTypes.Name);
		return username is null ? null : await _repository.UserAuthentication.GetUserAsync(username);
	}
}