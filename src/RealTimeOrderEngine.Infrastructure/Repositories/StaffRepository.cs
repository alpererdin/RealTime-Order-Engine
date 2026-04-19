using Microsoft.EntityFrameworkCore;
using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Infrastructure.Data;

namespace RealTimeOrderEngine.Infrastructure.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly ApplicationDbContext _context;

    public StaffRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Staff?> GetByPinAsync(string pinCode)
    {
        if (string.IsNullOrWhiteSpace(pinCode)) return null;

        var cleanPin = pinCode.Trim();
        
        return await _context.Staffs
            .FirstOrDefaultAsync(s => !s.IsDeleted && s.IsActive && s.PinCode.Trim() == cleanPin);
    }
}
