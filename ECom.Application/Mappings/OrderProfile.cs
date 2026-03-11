using AutoMapper;
using ECom.Application.DTOs.Order;
using ECom.Domain.Entities;

namespace ECom.Application.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<ShippingAddress, ShippingAddressDto>();

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.UnitPrice,
                opt => opt.MapFrom(src => src.UnitPrice))
            .ForSourceMember(src => src.Order, opt => opt.DoNotValidate())
            .ForSourceMember(src => src.Product, opt => opt.DoNotValidate());

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => src.Items))
            .ForSourceMember(src => src.User, opt => opt.DoNotValidate());
    }
}