using RealTimeOrderEngine.Shared.Contracts;

namespace RealTimeOrderEngine.Application.Interfaces.Services;

public interface IOrderNotificationService
{
    Task NotifyOrderCreatedAsync(OrderCreatedMessage message);
    Task NotifyOrderStatusChangedAsync(OrderStatusChangedMessage message);
    Task NotifyKitchenAsync(KitchenNotificationMessage message);

    Task NotifyReviewPermissionChangedAsync(Guid tableId, bool isAllowed);
} 