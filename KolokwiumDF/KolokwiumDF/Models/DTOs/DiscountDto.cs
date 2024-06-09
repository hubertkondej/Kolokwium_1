namespace KolowkiumDF.Models.DTOs;

public class DiscountDto
{
    public int Id { get; set; }
    public string Value { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
}