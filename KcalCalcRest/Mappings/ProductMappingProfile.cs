using AutoMapper;
using KcalCalcRest.DTOs;
using KcalCalcRest.Models;

namespace KcalCalcRest.Mappings; 

public class ProductMappingProfile : Profile {
	public ProductMappingProfile() {
		CreateMap<Product, ProductDTO>();
		CreateMap<ProductCreationAndUpdateDTO, Product>();
	}
}