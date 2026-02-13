namespace RealTimeOrderEngine.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 1,
        Approved = 2,
        Preparing = 3,
        Ready = 4,
        Served = 5,
        Completed = 6,
        Cancelled = 7
    }
}