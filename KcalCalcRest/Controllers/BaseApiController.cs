using AutoMapper;
using KcalCalcRest.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KcalCalcRest.Controllers; 

[Authorize(AuthenticationSchemes = "Bearer")]
public class BaseApiController : ControllerBase {
	protected readonly IRepositoryManager _repository;
	protected readonly IMapper _mapper;

	public BaseApiController(IRepositoryManager repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}
}