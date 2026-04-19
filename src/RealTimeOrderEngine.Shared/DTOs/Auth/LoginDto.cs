using System.ComponentModel.DataAnnotations;

namespace RealTimeOrderEngine.Shared.DTOs.Auth;

public class LoginDto
{
    [Required]
    [StringLength(12, MinimumLength = 4)]
    public string PinCode { get; set; } = string.Empty;
}
