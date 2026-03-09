using RealTimeOrderEngine.Domain.Common;

namespace RealTimeOrderEngine.Domain.Entities;

public class Review : BaseEntity
{
    public Guid ProductId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    
    public required Product Product { get; set; }

    public Guid OrderId { get; set; }
}