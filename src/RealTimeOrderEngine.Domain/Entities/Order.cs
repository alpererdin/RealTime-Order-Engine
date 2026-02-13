using RealTimeOrderEngine.Domain.Common;
using RealTimeOrderEngine.Domain.Enums;

namespace RealTimeOrderEngine.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid TableId { get; set; }
        public Guid? SessionId { get; set; }
        //public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.None;
        
        public required Table Table { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal TotalAmount => OrderItems.Where(x => !x.IsDeleted).Sum(x => x.Quantity * x.UnitPrice);
    }
}