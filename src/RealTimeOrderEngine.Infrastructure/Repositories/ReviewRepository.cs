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

    public async Task<bool> ExistsAsync(Guid productId, Guid orderId)
    {
        return await _context.Reviews.AnyAsync(r => r.ProductId == productId && r.OrderId == orderId);
    }

    public async Task<Review> AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId)
    {
        return await _context.Reviews
            .Where(r => r.ProductId == productId && !r.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .Where(r => !r.IsDeleted)
            .ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(Guid id)
    {
        return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
    }

    public async Task<bool> DeleteAsync(Review review)
    {
        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return true;
    }
}