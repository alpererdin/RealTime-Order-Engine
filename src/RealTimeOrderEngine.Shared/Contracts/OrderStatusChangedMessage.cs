namespace RealTimeOrderEngine.Shared.Contracts;

public class OrderStatusChangedMessage
{
    public Guid OrderId { get; set; }
    public required string NewStatus { get; set; }
}