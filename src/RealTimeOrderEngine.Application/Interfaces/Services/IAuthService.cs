using RealTimeOrderEngine.Shared.DTOs.Auth;

namespace RealTimeOrderEngine.Application.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
}