using Microsoft.EntityFrameworkCore;
using TechXpress.Data;
using TechXpress.Models;
using TechXpress.Repositories.Interfaces;

namespace TechXpress.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Category.ToListAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Category.FindAsync(id);
        }
        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Category.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            if (category == null) return false;
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
