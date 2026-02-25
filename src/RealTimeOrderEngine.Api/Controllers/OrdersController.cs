using Microsoft.AspNetCore.Mvc;
using RealTimeOrderEngine.Application.Services;
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
}