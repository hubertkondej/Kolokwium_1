using System.Collections.Generic;
using System.Threading.Tasks;
 namespace KolokwiumDF.Models.Interfaces;


public interface ISubscriptionRepository
{
    Task<Subscription> GetSubscriptionByIdAsync(int id);
    Task<List<Subscription>> GetSubscriptionsByClientIdAsync(int clientId);
    Task AddSubscriptionAsync(Subscription subscription);
    Task UpdateSubscriptionAsync(Subscription subscription);
    Task DeleteSubscriptionAsync(int id);
}
