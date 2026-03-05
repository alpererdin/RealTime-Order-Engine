namespace RealTimeOrderEngine.Shared.DTOs.Tables;

public class TableDto
    {
        public Guid Id { get; set; }
        public required string TableNumber { get; set; }
        public Guid? CurrentSessionId { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsReviewAllowed { get; set; }
    }