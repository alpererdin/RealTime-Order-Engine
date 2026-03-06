using RealTimeOrderEngine.Domain.Common;

namespace RealTimeOrderEngine.Domain.Entities;

public class Staff : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string PinCode { get; set; } = string.Empty;
    public string Role { get; set; } = "Waiter";
    public bool IsActive { get; set; } = true;
    public Staff()
    {
        IsDeleted = false;
    }
}