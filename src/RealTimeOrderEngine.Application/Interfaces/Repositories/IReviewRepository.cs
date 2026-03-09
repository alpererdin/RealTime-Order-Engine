using RealTimeOrderEngine.Domain.Entities;

namespace RealTimeOrderEngine.Application.Interfaces.Repositories;

public interface IReviewRepository
{
    Task<Review> AddAsync(Review review);
    Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId);

    Task<bool> ExistsAsync(Guid productId, Guid orderId);
}