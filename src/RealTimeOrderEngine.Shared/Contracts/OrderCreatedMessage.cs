namespace RealTimeOrderEngine.Shared.Contracts;

public class OrderCreatedMessage
{
    public Guid OrderId { get; set; }
    public required string TableNumber { get; set; }
    public DateTime CreatedAt { get; set; }
}