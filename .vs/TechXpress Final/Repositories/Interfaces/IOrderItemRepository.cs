using TechXpress.Models;

namespace TechXpress.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem> GetOrderItemByIdAsync(int id);
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
        Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem);
        Task<bool> DeleteOrderItemAsync(int id);
    }
}
