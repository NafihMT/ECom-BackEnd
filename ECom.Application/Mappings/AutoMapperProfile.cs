using AutoMapper;
using ECom.Application.DTOs.Category;
using ECom.Application.DTOs.Product;
using ECom.Application.DTOs.User;
using ECom.Domain.Entities;

namespace ECom.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.Category));

        CreateMap<Category, CategoryDto>();
        CreateMap<User, UserDto>();
    }
}

