using System.ComponentModel.DataAnnotations;

namespace RealTimeOrderEngine.Shared.DTOs.Orders;

public class CreateOrderDto
{
    [Required]
    public Guid TableId { get; set; }

    [MinLength(1)]
    public List<CreateOrderItemDto> Items { get; set; } = new();

    public Guid? SessionId { get; set; }
}
