using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace KolokwiumDF.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly SubscriptionContext _context;

    public ClientRepository(SubscriptionContext context)
    {
        _context = context;
    }

    public async Task<Client> GetClientWithSubscriptionsAsync(int id)
    {
        return await _context.Clients
            .Include(c => c.Discount)
            .Include(c => c.Subscriptions)
                .ThenInclude(s => s.Payments)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Client>> GetAllClientsAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task AddClientAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateClientAsync(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteClientAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client != null)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }
    }
}