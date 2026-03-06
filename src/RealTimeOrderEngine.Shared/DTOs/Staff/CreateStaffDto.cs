namespace RealTimeOrderEngine.Shared.DTOs.Staff;

public class CreateStaffDto
{
    public string Name { get; set; } = string.Empty;
    public string PinCode { get; set; } = string.Empty;
    public string Role { get; set; } = "Waiter";
}