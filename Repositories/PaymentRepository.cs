using Microsoft.EntityFrameworkCore;
using TechXpress.Data;
using TechXpress.Models;
using TechXpress.Repositories.Interfaces;

namespace TechXpress.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Payment>> GetAllPaymentAsync()
        {
            return await _context.Payments.ToListAsync();
        }
        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }
        public async Task<IEnumerable<Payment>> GetPaymentByUserIdAsync(int userId)
        {
            return await _context.Payments.Where(p => p.UserId == userId).ToListAsync();
        }
        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
        public async Task<Payment> UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
        public async Task<bool> DeletePaymentAsync(int id)
        {
            var payment = await GetPaymentByIdAsync(id);
            if (payment == null) return false;
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
