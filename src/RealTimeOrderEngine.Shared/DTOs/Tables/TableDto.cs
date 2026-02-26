namespace RealTimeOrderEngine.Shared.DTOs.Tables;

public class TableDto
{
    public Guid Id { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public bool IsOccupied { get; set; }
    public Guid? CurrentSessionId { get; set; }
}