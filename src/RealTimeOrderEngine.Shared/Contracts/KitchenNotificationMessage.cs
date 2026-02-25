namespace RealTimeOrderEngine.Shared.Contracts;

public class KitchenNotificationMessage
{
    public Guid OrderId { get; set; }
    public required string Message { get; set; }
}