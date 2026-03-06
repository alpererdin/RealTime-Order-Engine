using RealTimeOrderEngine.Domain.Entities;

namespace RealTimeOrderEngine.Application.Interfaces.Repositories;

public interface IStaffRepository 
{
    Task<Staff?> GetByPinAsync(string pinCode);
}