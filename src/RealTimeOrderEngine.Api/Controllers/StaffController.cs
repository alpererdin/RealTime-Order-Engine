using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Infrastructure.Data;
using RealTimeOrderEngine.Shared.DTOs.Staff;

namespace RealTimeOrderEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class StaffController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StaffController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StaffDto>>> GetStaff()
    {
        var staff = await _context.Staffs
            .Where(s => !s.IsDeleted)
            .Select(s => new StaffDto
            {
                Id = s.Id,
                Name = s.Name,
                Role = s.Role,
                IsActive = s.IsActive
            })
            .ToListAsync();

        return Ok(staff);
    }

    [HttpPost]
    public async Task<ActionResult> CreateStaff([FromBody] CreateStaffDto dto)
    {
        var staff = new Staff
        {
            Name = dto.Name,
            PinCode = dto.PinCode,
            Role = dto.Role,
            IsActive = true
        };

        _context.Staffs.Add(staff);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStaff(Guid id)
    {
        var staff = await _context.Staffs.FindAsync(id);
        if (staff == null) return NotFound();

        staff.IsDeleted = true;
        await _context.SaveChangesAsync();

        return Ok();
    }
}