namespace RealTimeOrderEngine.Shared.DTOs.Orders;

public class OrderDto
{
    public Guid Id { get; set; }
    public required string TableNumber { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public string? Status { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
}