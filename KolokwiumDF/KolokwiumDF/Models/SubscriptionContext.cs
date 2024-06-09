using Microsoft.EntityFrameworkCore;


namespace KolokwiumDF.Models;

public class SubscriptionContext : DbContext
{
    public SubscriptionContext(DbContextOptions<SubscriptionContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
}
