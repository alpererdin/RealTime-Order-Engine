using RealTimeOrderEngine.Domain.Entities;

namespace RealTimeOrderEngine.Application.Interfaces.Repositories;

public interface ITableRepository
{
    Task<IEnumerable<Table>> GetAllAsync();
    Task<Table?> GetByIdAsync(Guid id);
    Task UpdateAsync(Table table);
}