using Microsoft.EntityFrameworkCore;
using TechXpress.Data;
using TechXpress.Models;
using TechXpress.Repositories.Interfaces;

namespace TechXpress.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;
        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Review.ToListAsync();
        }
        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Review.FindAsync(id);
        }
        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(int productId)
        {
            return await _context.Review.Where(r => r.ProductId == productId).ToListAsync();
        }
        public async Task<Review> AddReviewAsync(Review review)
        {
            await _context.Review.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }
        public async Task<Review> UpdateReviewAsync(Review review)
        {
            _context.Review.Update(review);
            await _context.SaveChangesAsync();
            return review;
        }
        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await GetReviewByIdAsync(id);
            if (review == null) return false;
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
