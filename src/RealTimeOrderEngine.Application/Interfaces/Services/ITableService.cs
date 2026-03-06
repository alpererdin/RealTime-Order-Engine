using RealTimeOrderEngine.Shared.DTOs.Tables;

namespace RealTimeOrderEngine.Application.Interfaces.Services;

public interface ITableService
{
    Task<IEnumerable<TableDto>> GetAllTablesAsync();
    Task<TableDto?> OpenTableSessionAsync(Guid id);
    Task<bool> CloseTableSessionAsync(Guid id);
    Task<bool> ValidateSessionAsync(Guid tableId, Guid sessionId);

    Task<TableDto?> GetTableByIdAsync(Guid id);
    Task<bool> UpdateReviewPermissionAsync(Guid id, bool isAllowed);
}