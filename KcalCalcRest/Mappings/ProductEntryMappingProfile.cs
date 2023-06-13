using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Models;

namespace KcalCalcRest.Mappings; 

public class ProductEntryMappingProfile : Profile {
	public ProductEntryMappingProfile() {
		CreateMap<ProductEntryCreationDTO, ProductEntry>();
		CreateMap<ProductEntry, ProductEntryDTO>();
	}
}