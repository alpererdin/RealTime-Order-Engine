using RealTimeOrderEngine.Domain.Common;

namespace RealTimeOrderEngine.Domain.Entities
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}