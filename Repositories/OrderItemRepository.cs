using TechXpress.Repositories.Interfaces;
using TechXpress.Models;
using TechXpress.Data;
using Microsoft.EntityFrameworkCore;

namespace TechXpress.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly AppDbContext _context;
        public OrderItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await _context.OrderItem
                .Include(oi => oi.Product)
                .ToListAsync();
        }
        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await _context.OrderItem
                .Include(oi => oi.Product)
                .FirstOrDefaultAsync(oi => oi.OrderItemId == id);
        }
        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _context.OrderItem
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Product)
                .ToListAsync();
        }
        public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItem.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }
        public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItem.Update(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }
        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var orderItem = await GetOrderItemByIdAsync(id);
            if (orderItem == null)
                return false;
            _context.OrderItem.Remove(orderItem);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
