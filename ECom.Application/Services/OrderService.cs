using AutoMapper;
using ECom.Application.DTOs.Order;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Entities;
using ECom.Domain.Enums;
using ECom.Domain.Interfaces.Repositories;

namespace ECom.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> PlaceOrderAsync(int userId, CreateOrderDto dto)
    {
        if (dto.Items == null || !dto.Items.Any())
            throw new ArgumentException("Order must have at least one item");

        var order = new Order
        {
            UserId = userId,
            Status = OrderStatus.Pending,
            Items = new List<OrderItem>(),
            ShippingAddress = new ShippingAddress
            {
                F_Name = dto.ShippingAddress.f_name,
                Address = dto.ShippingAddress.address,
                City = dto.ShippingAddress.city,
                ZipCode = dto.ShippingAddress.zipCode
            }
        };

        decimal total = 0m;

        foreach (var itemDto in dto.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
            if (product == null)
                throw new ArgumentException($"Product {itemDto.ProductId} not found");

            var item = new OrderItem
            {
                ProductId = product.Id,
                Quantity = itemDto.Quantity,
                UnitPrice = product.Price
            };

            order.Items.Add(item);
            total += product.Price * itemDto.Quantity;
        }

        order.TotalAmount = total;

        var created = await _orderRepository.CreateAsync(order);

        return _mapper.Map<OrderDto>(created);
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return order == null ? null : _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetByUserAsync(int userId)
    {
        var orders = await _orderRepository.GetByUserAsync(userId);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto> UpdateStatusAsync(int orderId, OrderStatus status)
    {

        if (!Enum.IsDefined(typeof(OrderStatus), status))
            throw new ArgumentException("Invalid order status");

        var order = await _orderRepository.GetByIdAsync(orderId);


        if (order == null)
            throw new Exception("Order not found");

        order.Status = status;   

        await _orderRepository.SaveChangesAsync();

        return new OrderDto
        {
            Id = order.Id,
            Status = order.Status.ToString(),   
            TotalAmount = order.TotalAmount,
            CreatedAt = order.CreatedAt
        };
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        return await _orderRepository.GetTotalRevenueAsync();
    }


}
