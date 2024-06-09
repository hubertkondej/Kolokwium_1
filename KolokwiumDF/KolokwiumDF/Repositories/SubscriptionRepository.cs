using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace KolokwiumDF.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly SubscriptionContext _context;

    public SubscriptionRepository(SubscriptionContext context)
    {
        _context = context;
    }

    public async Task<Subscription> GetSubscriptionByIdAsync(int id)
    {
        return await _context.Subscriptions.FindAsync(id);
    }

    public async Task<List<Subscription>> GetSubscriptionsByClientIdAsync(int clientId)
    {
        return await _context.Subscriptions
            .Where(s => s.ClientId == clientId)
            .ToListAsync();
    }

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateSubscriptionAsync(Subscription subscription)
    {
        _context.Subscriptions.Update(subscription);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSubscriptionAsync(int id)
    {
        var subscription = await _context.Subscriptions.FindAsync(id);
        if (subscription != null)
        {
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }
    }
}
