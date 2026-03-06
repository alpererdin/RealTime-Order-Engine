namespace RealTimeOrderEngine.Shared.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string StaffName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}