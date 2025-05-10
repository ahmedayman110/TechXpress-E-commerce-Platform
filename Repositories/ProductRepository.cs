using Microsoft.EntityFrameworkCore;
using TechXpress.Data;
using TechXpress.Models;
using TechXpress.Repositories.Interfaces;

namespace TechXpress.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Product.ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Product.FindAsync(id);
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Product
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _context.Product
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> UpdateProductAsync(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null)
                return false;
            
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
