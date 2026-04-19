using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Application.Exceptions;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Domain.Enums;
using RealTimeOrderEngine.Shared.Contracts;
using RealTimeOrderEngine.Shared.DTOs.Orders;

namespace RealTimeOrderEngine.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderNotificationService _notificationService;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IOrderNotificationService notificationService, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _notificationService = notificationService;
        _productRepository = productRepository;
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
    {
        if (dto.Items.Count == 0)
        {
            throw new BusinessRuleException("An order must contain at least one item.");
        }

        var orderItems = new List<OrderItem>();
        
        foreach (var item in dto.Items)
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);
            
            if (product == null)
            {
                throw new ResourceNotFoundException("Product not found.");
            }
            
            if (!product.IsAvailable)  
            {
                throw new BusinessRuleException($"{product.Name} is currently unavailable.");
            }

            if (product.IsStockTracked)
            {
                if (product.StockQuantity < item.Quantity)
                {
                    throw new BusinessRuleException("Insufficient stock for product: " + product.Name);
                }
                
                product.StockQuantity -= item.Quantity;
                await _productRepository.UpdateAsync(product);
            }

            orderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Note = item.Note,
                UnitPrice = product.Price
            });
        }

        var order = new Order
        {
            TableId = dto.TableId,
            SessionId = dto.SessionId,
            Table = null!,
            OrderItems = orderItems
        };

        var createdOrder = await _orderRepository.AddAsync(order);

        var message = new OrderCreatedMessage
        {
            OrderId = createdOrder.Id,
            TableNumber = createdOrder.Table?.TableNumber ?? createdOrder.TableId.ToString(),
            CreatedAt = DateTime.UtcNow
        };
        await _notificationService.NotifyOrderCreatedAsync(message);

        return new OrderDto
        {
            Id = createdOrder.Id,
            TableId = createdOrder.TableId,
            SessionId = createdOrder.SessionId,
            TableNumber = createdOrder.Table?.TableNumber ?? createdOrder.TableId.ToString(),
            OrderDate = DateTime.UtcNow,
            TotalAmount = createdOrder.TotalAmount
        };
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            TableId = o.TableId,
            SessionId = o.SessionId, 
            TableNumber = o.Table?.TableNumber ?? o.TableId.ToString(),
            TotalAmount = o.TotalAmount,
            OrderDate = o.CreatedAt,
            Status = o.Status.ToString(),
            Items = o.OrderItems?.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product?.Name,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Note = i.Note
            }).ToList() ?? new List<OrderItemDto>()
        });
    }

    public async Task<bool> UpdateOrderStatusAsync(Guid id, OrderStatus newStatus)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null) return false;

        order.Status = newStatus;
        await _orderRepository.UpdateAsync(order);

        var message = new OrderStatusChangedMessage
        {
            OrderId = order.Id,
            NewStatus = newStatus.ToString()
        };
        await _notificationService.NotifyOrderStatusChangedAsync(message);
        return true;
    }
}
