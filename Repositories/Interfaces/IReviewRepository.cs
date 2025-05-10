using TechXpress.Models;

namespace TechXpress.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId);
        Task<Review> AddReviewAsync(Review review);
        Task<Review> UpdateReviewAsync(Review review);
        Task<bool> DeleteReviewAsync(int id);
    }
}
