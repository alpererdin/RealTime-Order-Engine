namespace RealTimeOrderEngine.Shared.DTOs.Orders;

public class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public string? Note { get; set; }
}