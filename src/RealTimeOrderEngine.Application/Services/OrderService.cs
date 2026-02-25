using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Shared.DTOs.Orders;
using RealTimeOrderEngine.Shared.Contracts;

namespace RealTimeOrderEngine.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderNotificationService _notificationService;

    public OrderService(IOrderRepository orderRepository, IOrderNotificationService notificationService)
    {
        _orderRepository = orderRepository;
        _notificationService = notificationService;
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            TableId = dto.TableId,
            Table = null!
        };

        var createdOrder = await _orderRepository.AddAsync(order);

        var message = new OrderCreatedMessage
        {
            OrderId = createdOrder.Id,
            TableNumber = createdOrder.TableId.ToString(),
            CreatedAt = DateTime.UtcNow
        };

        await _notificationService.NotifyOrderCreatedAsync(message);

        return new OrderDto
        {
            Id = createdOrder.Id,
            TableNumber = createdOrder.TableId.ToString(),
            OrderDate = DateTime.UtcNow,
            TotalAmount = createdOrder.TotalAmount
        };
    }
}