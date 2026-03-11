using RealTimeOrderEngine.Shared.DTOs.Reviews;

namespace RealTimeOrderEngine.Application.Interfaces.Services;

public interface IReviewService
{
    Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto);
    Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(Guid productId);
    Task<IEnumerable<ReviewDto>> GetAllReviewsAsync();
    Task<bool> DeleteReviewAsync(Guid id);
}