namespace RealTimeOrderEngine.Shared.DTOs.Reviews;

public class CreateReviewDto
{
    public Guid ProductId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
     public Guid OrderId { get; set; }
}