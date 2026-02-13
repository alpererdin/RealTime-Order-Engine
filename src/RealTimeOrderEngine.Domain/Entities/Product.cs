using RealTimeOrderEngine.Domain.Common;

namespace RealTimeOrderEngine.Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public decimal Price { get; set; } 
        public Guid CategoryId { get; set; }
        public bool IsAvailable { get; set; } = true;
        
    
        public required Category Category { get; set; }
    }
}