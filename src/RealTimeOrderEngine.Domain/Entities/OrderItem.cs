using RealTimeOrderEngine.Domain.Common;
using RealTimeOrderEngine.Domain.Enums;

namespace RealTimeOrderEngine.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Note { get; set; }
        
        public required Order Order { get; set; }
        public required Product Product { get; set; }

        public OrderItemStatus Status { get; set; } = OrderItemStatus.Pending;
    }
}