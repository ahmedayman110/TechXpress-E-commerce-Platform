using Microsoft.EntityFrameworkCore;
using TechXpress.Data;
using TechXpress.Models;
using TechXpress.Repositories.Interfaces;

namespace TechXpress.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Cart>> GetAllCartsAsync()
        {
            return await _context.Cart
                .Include(c => c.User)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();
        }
        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await _context.Cart
                .Include(c => c.User)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.CartId == id);
        }
        public async Task<Cart> AddCartAsync(Cart cart)
        {
            await _context.Cart.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            _context.Cart.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
        public async Task<bool> DeleteCartAsync(int id)
        {
            var cart = await GetCartByIdAsync(id);
            if (cart == null)
                return false;
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Cart
                .Include(c => c.User)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task<bool> ClearCartAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null || !cart.CartItems.Any())
                return false;
            cart.CartItems.Clear();
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
