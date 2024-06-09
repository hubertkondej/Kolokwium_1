using Microsoft.AspNetCore.Mvc;
 namespace KolokwiumDF.Controllers;



[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly SubscriptionContext _context;

    public ClientsController(SubscriptionContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetClientSubscriptions(int id)
    {
        var client = await _context.Clients
            .Include(c => c.Discount)
            .Include(c => c.Subscriptions)
                .ThenInclude(s => s.Payments)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (client == null)
        {
            return NotFound();
        }

        var clientDto = new ClientDto
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            Phone = client.Phone,
            Discount = client.Discount?.Value,
            Subscriptions = client.Subscriptions.Select(s => new SubscriptionDto
            {
                IdSubscription = s.Id,
                Name = s.Name,
                RenewalPeriod = s.RenewalPeriod,
                TotalPaidAmount = s.Payments.Sum(p => p.Amount)
            }).ToList()
        };

        return clientDto;
    }
}