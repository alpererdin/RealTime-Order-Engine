namespace RealTimeOrderEngine.Shared.DTOs.Orders;

public class CreateOrderDto
{
    public Guid TableId { get; set; }
    public List<CreateOrderItemDto> Items { get; set; } = new();
}