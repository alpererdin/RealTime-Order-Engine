using Microsoft.EntityFrameworkCore;
using RealTimeOrderEngine.Application.Interfaces.Repositories;
using RealTimeOrderEngine.Domain.Entities;
using RealTimeOrderEngine.Infrastructure.Data;

namespace RealTimeOrderEngine.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Table)
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
            .ToListAsync();
    }

    public async Task<Order> AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return await _context.Orders
            .Include(o => o.Table)
            .FirstAsync(o => o.Id == order.Id);
    }
    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }
}