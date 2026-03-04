using ECom.Application.DTOs.Order;
using ECom.Application.Interfaces.Services;
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
        return Ok(order);
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
        return Ok(orders);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    
    [Authorize(Roles = "Admin")]     

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, ECom.Domain.Enums.OrderStatus status)
    {
        await _orderService.UpdateStatusAsync(id, status);
        return Ok(new { message = "Status updated" });
    }
}