using Microsoft.EntityFrameworkCore;
using TechXpress.Data;
using TechXpress.Models;
using TechXpress.Repositories.Interfaces;

namespace TechXpress.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Order // Fixed DbSet name to plural
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Order 
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId) 
        {
            return await _context.Order
                .Where(o => o.UserId == userId) 
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }


        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Order.AddAsync(order); 
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _context.Order.Update(order); 
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await GetOrderByIdAsync(id);
            if (order == null)
                return false;

            _context.Order.Remove(order); 
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
