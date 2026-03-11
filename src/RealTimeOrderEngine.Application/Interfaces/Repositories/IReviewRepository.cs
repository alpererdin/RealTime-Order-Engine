using RealTimeOrderEngine.Domain.Entities;

namespace RealTimeOrderEngine.Application.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<bool> ExistsAsync(Guid productId, Guid orderId);
    Task<Review> AddAsync(Review review);
    Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId);
    Task<IEnumerable<Review>> GetAllAsync();
    Task<Review?> GetByIdAsync(Guid id);
    Task<bool> DeleteAsync(Review review);
}