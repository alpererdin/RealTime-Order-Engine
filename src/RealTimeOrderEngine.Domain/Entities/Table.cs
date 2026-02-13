using RealTimeOrderEngine.Domain.Common;

namespace RealTimeOrderEngine.Domain.Entities
{
    public class Table : BaseEntity
    {
        public required string TableNumber { get; set; }
        public Guid? CurrentSessionId { get; set; }
        public bool IsOccupied { get; set; } = false;
    }
}