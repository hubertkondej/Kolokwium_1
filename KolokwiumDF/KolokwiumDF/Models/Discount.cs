 namespace KolokwiumDF.Models;

public class Discount
{
    public int Id { get; set; }
    public string Value { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
}