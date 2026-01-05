using AutoMapper;
using ECom.Application.DTOs.Product;
using ECom.Domain.Entities;

namespace ECom.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(
                dest => dest.Category,
                opt => opt.MapFrom(src => src.Category.Name)
            );

    }
}
