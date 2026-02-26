using Microsoft.AspNetCore.Mvc;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Shared.DTOs.Tables;

namespace RealTimeOrderEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TablesController : ControllerBase
{
    private readonly ITableService _tableService;

    public TablesController(ITableService tableService)
    {
        _tableService = tableService;
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
}