using Microsoft.EntityFrameworkCore;
using TechXpress.Data;
using TechXpress.Models;
using TechXpress.Repositories.Interfaces;

namespace TechXpress.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;
        public CartItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CartItem>> GetAllCartItemsAsync()
        {
            return await _context.CartItem
                .Include(ci => ci.Product)
                .ToListAsync();
        }
        public async Task<CartItem> GetCartItemByIdAsync(int id)
        {
            return await _context.CartItem
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.CartItemId == id);
        }
        public async Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            return await _context.CartItem
                .Include(ci => ci.Product)
                .Where(ci => ci.CartId == cartId)
                .ToListAsync();
        }
        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            await _context.CartItem.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
        public async Task<CartItem> UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItem.Update(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }
        public async Task<bool> DeleteCartItemAsync(int id)
        {
            var cartItem = await GetCartItemByIdAsync(id);
            if (cartItem == null)
                return false;
            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
