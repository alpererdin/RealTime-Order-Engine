using Microsoft.EntityFrameworkCore;
using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Infrastructure.Data;

namespace RealTimeOrderEngine.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;

    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Review> AddAsync(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId)
    {
        return await _context.Reviews
            .Where(r => r.ProductId == productId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }
}