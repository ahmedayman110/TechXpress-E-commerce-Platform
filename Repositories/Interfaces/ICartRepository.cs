using TechXpress.Models;

namespace TechXpress.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task<Cart> GetCartByIdAsync(int id);
        Task<Cart> AddCartAsync(Cart cart);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(int id);
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task<bool> ClearCartAsync(int userId);
    }
}
