using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Shared.DTOs.Tables;

namespace RealTimeOrderEngine.Application.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    public TableService(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task<IEnumerable<TableDto>> GetAllTablesAsync()
    {
        var tables = await _tableRepository.GetAllAsync();
        return tables.Select(t => new TableDto
        {
            Id = t.Id,
            TableNumber = t.TableNumber,
            IsOccupied = t.IsOccupied,
            CurrentSessionId = t.CurrentSessionId
        });
    }

    public async Task<TableDto?> OpenTableSessionAsync(Guid id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null) return null;

        table.IsOccupied = true;
        table.CurrentSessionId = Guid.NewGuid();
        await _tableRepository.UpdateAsync(table);

        return new TableDto
        {
            Id = table.Id,
            TableNumber = table.TableNumber,
            IsOccupied = table.IsOccupied,
            CurrentSessionId = table.CurrentSessionId
        };
    }

    public async Task<bool> CloseTableSessionAsync(Guid id)
    {
        var table = await _tableRepository.GetByIdAsync(id);
        if (table == null || !table.IsOccupied) return false;

        table.IsOccupied = false;
        table.CurrentSessionId = null;
        await _tableRepository.UpdateAsync(table);

        return true;
    }

    public async Task<bool> ValidateSessionAsync(Guid tableId, Guid sessionId)
    {
        var table = await _tableRepository.GetByIdAsync(tableId);
        return table != null && table.IsOccupied && table.CurrentSessionId == sessionId;
    }

    public async Task<TableDto?> GetTableByIdAsync(Guid id)
{
    var table = await _tableRepository.GetByIdAsync(id);
    if (table == null) return null;
    return new TableDto
    {
        Id = table.Id,
        TableNumber = table.TableNumber,
        IsOccupied = table.IsOccupied,
        CurrentSessionId = table.CurrentSessionId
    };
}
}