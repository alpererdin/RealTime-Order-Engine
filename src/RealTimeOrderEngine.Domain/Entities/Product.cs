using RealTimeOrderEngine.Domain.Common;

namespace RealTimeOrderEngine.Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; } 
        public Guid CategoryId { get; set; }
        public bool IsAvailable { get; set; } = true;
        
        public required Category Category { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public int StockQuantity { get; set; }
        public bool IsStockTracked { get; set; }
    }
}