 namespace KolokwiumDF.Models;

public class Payment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
}