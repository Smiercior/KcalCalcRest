using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Models;

namespace KcalCalcRest.Mappings; 

public class UserMappingProfile : Profile {
	public UserMappingProfile() {
		CreateMap<UserRegistrationDTO, User>();
	}
}