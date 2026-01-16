using AutoMapper;
using ECom.Application.DTOs.Product;
using ECom.Domain.Entities;

namespace ECom.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<Product, ProductDto>();
        CreateMap<UpdateProductDto, Product>();
    }
}
