using System.Collections.Generic;
using System.Threading.Tasks;
 namespace  KolokwiumDF.Models.Interfaces;
public interface IClientRepository
{
    Task<Client> GetClientWithSubscriptionsAsync(int id);
    Task<List<Client>> GetAllClientsAsync();
    Task AddClientAsync(Client client);
    Task UpdateClientAsync(Client client);
    Task DeleteClientAsync(int id);
}
