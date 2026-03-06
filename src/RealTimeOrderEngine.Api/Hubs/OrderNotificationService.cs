using Microsoft.AspNetCore.SignalR;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Shared.Contracts;

namespace RealTimeOrderEngine.Api.Hubs;

public class OrderNotificationService : IOrderNotificationService
{
    private readonly IHubContext<OrderHub> _hubContext;

    public OrderNotificationService(IHubContext<OrderHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyOrderCreatedAsync(OrderCreatedMessage message)
    {
        await _hubContext.Clients.All.SendAsync("OrderCreated", message);
    }

    public async Task NotifyOrderStatusChangedAsync(OrderStatusChangedMessage message)
    {
        await _hubContext.Clients.All.SendAsync("OrderStatusChanged", message);
    }

    public async Task NotifyKitchenAsync(KitchenNotificationMessage message)
    {
        await _hubContext.Clients.All.SendAsync("KitchenNotification", message);
    }
    public async Task NotifyReviewPermissionChangedAsync(Guid tableId, bool isAllowed)
    {
        await _hubContext.Clients.All.SendAsync("ReviewPermissionChanged", tableId, isAllowed);
    }
}