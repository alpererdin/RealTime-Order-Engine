using Microsoft.AspNetCore.Mvc;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Shared.DTOs.Tables;
using Microsoft.AspNetCore.Authorization;
using RealTimeOrderEngine.Infrastructure.Data;
using RealTimeOrderEngine.Domain.Entities;

namespace RealTimeOrderEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TablesController : ControllerBase
{
    private readonly ITableService _tableService;
    private readonly IOrderNotificationService _notificationService;
    private readonly ApplicationDbContext _context;

    public TablesController(ITableService tableService, IOrderNotificationService notificationService, ApplicationDbContext context)
    {
        _tableService = tableService;
        _notificationService = notificationService;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TableDto>>> GetTables()
    {
        var tables = await _tableService.GetAllTablesAsync();
        return Ok(tables);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TableDto>> GetTable(Guid id)
    {
        var table = await _tableService.GetTableByIdAsync(id);
        if (table == null) return NotFound();
        return Ok(table);
    }

    [HttpPost("{id}/open")]
    public async Task<ActionResult<TableDto>> OpenTable(Guid id)
    {
        var table = await _tableService.OpenTableSessionAsync(id);
        if (table == null) return NotFound();
        return Ok(table);
    }

    [HttpPost("{id}/close")]
    public async Task<IActionResult> CloseTable(Guid id)
    {
        var success = await _tableService.CloseTableSessionAsync(id);
        if (!success) return BadRequest();
        return NoContent();
    }

    [HttpGet("{id}/validate")]
    public async Task<ActionResult<bool>> ValidateSession(Guid id, [FromQuery] Guid sessionId)
    {
        var isValid = await _tableService.ValidateSessionAsync(id, sessionId);
        return Ok(isValid);
    }

    [HttpPut("{id}/review-permission")]
    public async Task<IActionResult> UpdateReviewPermission(Guid id, [FromQuery] bool isAllowed)
    {
        var success = await _tableService.UpdateReviewPermissionAsync(id, isAllowed);
        if (!success) return NotFound();

        await _notificationService.NotifyReviewPermissionChangedAsync(id, isAllowed);

        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateTable([FromBody] CreateTableDto dto)
    {
        var table = new Table
        {
            TableNumber = dto.TableNumber
        };

        _context.Tables.Add(table);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteTable(Guid id)
    {
        var table = await _context.Tables.FindAsync(id);
        if (table == null) return NotFound();

        _context.Tables.Remove(table);
        await _context.SaveChangesAsync();

        return Ok();
    }
}