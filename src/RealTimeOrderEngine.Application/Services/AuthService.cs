using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Shared.DTOs.Auth;

namespace RealTimeOrderEngine.Application.Services;

public class AuthService : IAuthService
{
    private readonly IStaffRepository _staffRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IStaffRepository staffRepository, ITokenService tokenService)
    {
        _staffRepository = staffRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
    {
        var staff = await _staffRepository.GetByPinAsync(loginDto.PinCode);
        
        if (staff == null || !staff.IsActive || staff.IsDeleted)
        {
            return null;
        }

        var token = _tokenService.GenerateToken(staff);

        return new AuthResponseDto
        {
            Token = token,
            StaffName = staff.Name,
            Role = staff.Role
        };
    }
}
