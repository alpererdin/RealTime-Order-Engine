using RealTimeOrderEngine.Domain.Entities;

namespace RealTimeOrderEngine.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(Staff staff);
}