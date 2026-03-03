using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Application.Interfaces.Services;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Shared.DTOs.Reviews;

namespace RealTimeOrderEngine.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto)
    {
        var review = new Review
        {
            ProductId = dto.ProductId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            Product = null!
        };

        var createdReview = await _reviewRepository.AddAsync(review);

        return new ReviewDto
        {
            Id = createdReview.Id,
            ProductId = createdReview.ProductId,
            Rating = createdReview.Rating,
            Comment = createdReview.Comment,
            CreatedAt = createdReview.CreatedAt
        };
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsByProductIdAsync(Guid productId)
    {
        var reviews = await _reviewRepository.GetByProductIdAsync(productId);

        return reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            ProductId = r.ProductId,
            Rating = r.Rating,
            Comment = r.Comment,
            CreatedAt = r.CreatedAt
        });
    }
}