using System.ComponentModel.DataAnnotations;

namespace RealTimeOrderEngine.Shared.DTOs.Orders;

public class CreateOrderItemDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; }

    [StringLength(200)]
    public string? Note { get; set; }
}
