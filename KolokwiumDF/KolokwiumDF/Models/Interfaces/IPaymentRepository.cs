using System.Collections.Generic;
using System.Threading.Tasks;
namespace KolokwiumDF.Models.Interfaces;


public interface IPaymentRepository
{
    Task<Payment> GetPaymentByIdAsync(int id);
    Task<List<Payment>> GetPaymentsBySubscriptionIdAsync(int subscriptionId);
    Task AddPaymentAsync(Payment payment);
    Task UpdatePaymentAsync(Payment payment);
    Task DeletePaymentAsync(int id);
}
