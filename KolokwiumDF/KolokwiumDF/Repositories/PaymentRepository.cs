using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace KolokwiumDF.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly SubscriptionContext _context;

    public PaymentRepository(SubscriptionContext context)
    {
        _context = context;
    }

    public async Task<Payment> GetPaymentByIdAsync(int id)
    {
        return await _context.Payments.FindAsync(id);
    }

    public async Task<List<Payment>> GetPaymentsBySubscriptionIdAsync(int subscriptionId)
    {
        return await _context.Payments
            .Where(p => p.SubscriptionId == subscriptionId)
            .ToListAsync();
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePaymentAsync(Payment payment)
    {
        _context.Payments.Update(payment);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePaymentAsync(int id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment != null)
        {
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
        }
    }
}