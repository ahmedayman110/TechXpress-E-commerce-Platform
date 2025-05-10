using TechXpress.Models;

namespace TechXpress.Repositories.Interfaces
{
    public interface IWishlistRepository
    {
        Task<IEnumerable<Wishlist>> GetAllWishlistsAsync();
        Task<Wishlist> GetWishlistByIdAsync(int id);
        Task<IEnumerable<Wishlist>> GetWishlistsByUserIdAsync(int userId);
        Task<Wishlist> AddWishlistAsync(Wishlist wishlist);

        Task<Wishlist> AddProductToWishlistAsync(int wishlistId, int productId);
        Task<Wishlist> RemoveProductFromWishlistAsync(int wishlistId, int productId);
        Task<Wishlist> ClearWishlistAsync(int wishlistId);

        Task<Wishlist> UpdateWishlistAsync(Wishlist wishlist);
        Task<bool> DeleteWishlistAsync(int id);
    }
}
