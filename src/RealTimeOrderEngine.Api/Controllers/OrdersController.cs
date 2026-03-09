using Microsoft.AspNetCore.Mvc;
using RealTimeOrderEngine.Application.Services;
using RealTimeOrderEngine.Domain.Enums;
using RealTimeOrderEngine.Shared.DTOs.Orders;

namespace RealTimeOrderEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var order = await _orderService.CreateOrderAsync(dto);
        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] OrderStatus newStatus)
    {
        var result = await _orderService.UpdateOrderStatusAsync(id, newStatus);
        if (!result)
            return NotFound();

        return NoContent();
    }
}