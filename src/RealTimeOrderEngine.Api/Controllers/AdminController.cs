using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeOrderEngine.Infrastructure.Data;

namespace RealTimeOrderEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("query")]
    public async Task<IActionResult> ExecuteQuery([FromBody] QueryRequestDto dto)
    {
        var sql = dto.Query.Trim();

        if (!sql.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            return BadRequest(new { message = "Only SELECT queries are allowed." });

        var results = new List<Dictionary<string, object?>>();

        using var command = _context.Database.GetDbConnection().CreateCommand();
        command.CommandText = sql;

        await _context.Database.OpenConnectionAsync();

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var row = new Dictionary<string, object?>();
            for (int i = 0; i < reader.FieldCount; i++)
                row[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
            results.Add(row);
        }

        return Ok(results);
    }
}

public class QueryRequestDto
{
    public string Query { get; set; } = string.Empty;
}