using ECom.Application.Common;
using ECom.Application.DTOs.Order;
using ECom.Application.Interfaces.Services;
using ECom.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECom.Api.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(CreateOrderDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized();

        var order = await _orderService.PlaceOrderAsync(userId, dto);
        return Ok(new ApiResponse<object>(200, "Order Placed", order));
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
            return Unauthorized();

        if (!int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized();

        var orders = await _orderService.GetByUserAsync(userId);
        return Ok(new ApiResponse<object>(200, "Fetched Successfully", orders));
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(new ApiResponse<object>(200, "Fetched Successfully", order));
    }

    
    [Authorize(Roles = "Admin")]

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateOrderStatusDto dto)
    {
        var updatedOrder = await _orderService.UpdateStatusAsync(id, dto.Status);

        return Ok(new ApiResponse<OrderDto>(
            200,
            "Status Updated",
            updatedOrder
        ));
    }

    //[Authorize(Roles = "Admin")]
    //[HttpGet("all")]
    //public async Task<IActionResult> GetAllOrders()
    //{
    //    var orders = await _orderService.GetAllAsync();
    //    return Ok(new ApiResponse<object>(200, "Fetched Successfully", orders));
    //}

    [Authorize(Roles = "Admin")]
    [HttpGet("revenue")]
    public async Task<IActionResult> GetTotalRevenue()
    {
        var totalRevenue = await _orderService.GetTotalRevenueAsync();

        return Ok(new ApiResponse<decimal>(
            200,
            "Total Revenue Fetched",
            totalRevenue
        ));
    }


}