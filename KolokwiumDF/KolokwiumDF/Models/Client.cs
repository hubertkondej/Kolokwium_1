 namespace KolokwiumDF.Models;

public class Client
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Discount Discount { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
}