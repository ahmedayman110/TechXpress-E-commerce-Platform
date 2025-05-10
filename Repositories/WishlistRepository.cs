using Microsoft.EntityFrameworkCore;
using TechXpress.Data;
using TechXpress.Models;
using TechXpress.Repositories.Interfaces;

namespace TechXpress.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly AppDbContext _context;
        public WishlistRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Wishlist>> GetAllWishlistsAsync()
        {
            return await _context.Wishlist
                .Include(w => w.Product)
                .ToListAsync();
        }
        public async Task<Wishlist> GetWishlistByIdAsync(int id)
        {
            return await _context.Wishlist
                .Include(w => w.Product)
                .FirstOrDefaultAsync(w => w.WishlistId == id);
        }
        public async Task<IEnumerable<Wishlist>> GetWishlistsByUserIdAsync(int userId)
        {
            return await _context.Wishlist
                .Include(w => w.Product)
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }
        public async Task<Wishlist> AddWishlistAsync(Wishlist wishlist)
        {
            await _context.Wishlist.AddAsync(wishlist);
            await _context.SaveChangesAsync();
            return wishlist;
        }

        public async Task<Wishlist> AddProductToWishlistAsync(int wishlistId, int productId)
        {
            var wishlist = await _context.Wishlist.FirstOrDefaultAsync(w => w.WishlistId == wishlistId);
            if (wishlist == null)
                throw new ArgumentException("Wishlist not found.");

            var product = await _context.Product.FirstOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
                throw new ArgumentException("Product not found.");

            var newWishlistItem = new Wishlist
            {
                WishlistId = wishlistId,
                ProductId = productId,
                UserId = wishlist.UserId
            };

            await _context.Wishlist.AddAsync(newWishlistItem);
            await _context.SaveChangesAsync();
            return newWishlistItem;
        }

        public async Task<Wishlist> RemoveProductFromWishlistAsync(int wishlistId, int productId)
        {
            var wishlistItem = await _context.Wishlist
                .FirstOrDefaultAsync(w => w.WishlistId == wishlistId && w.ProductId == productId);

            if (wishlistItem == null)
                throw new ArgumentException("Wishlist item not found.");

            _context.Wishlist.Remove(wishlistItem);
            await _context.SaveChangesAsync();
            return wishlistItem;
        }

        public async Task<Wishlist> ClearWishlistAsync(int wishlistId)
        {
            var wishlistItems = await _context.Wishlist
                .Where(w => w.WishlistId == wishlistId)
                .ToListAsync();

            if (!wishlistItems.Any())
                throw new ArgumentException("Wishlist is already empty or not found.");

            _context.Wishlist.RemoveRange(wishlistItems);
            await _context.SaveChangesAsync();
            return null; 
        }


        public async Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist)
        {
            _context.Wishlist.Update(wishlist);
            await _context.SaveChangesAsync();
            return wishlist;
        }
        public async Task<bool> DeleteWishlistAsync(int id)
        {
            var wishlist = await GetWishlistByIdAsync(id);
            if (wishlist == null)
                return false;
            _context.Wishlist.Remove(wishlist);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
