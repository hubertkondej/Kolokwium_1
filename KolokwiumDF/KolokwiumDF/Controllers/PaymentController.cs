using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using KolokwiumDF.Controllers;  

namespace KolokwiumDF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly baza _context;

        public PaymentController(baza context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDto paymentDto)
        {
            var client = await _context.Clients.FindAsync(paymentDto.IdClient);
            if (client == null)
            {
                return NotFound(new { message = "Klient nie istnieje." });
            }

            var subscription = await _context.Subscriptions.FindAsync(paymentDto.IdSubscription);
            if (subscription == null)
            {
                return NotFound(new { message = "Subskrypcja nie istnieje." });
            }

            if (subscription.EndTime < DateTime.UtcNow)
            {
                return BadRequest(new { message = "Subskrypcja nieaktywna" });
            }

            var existingPayment = await _context.Payments
                .Where(p => p.IdClient == paymentDto.IdClient && p.IdSubscription == paymentDto.IdSubscription && p.Date >= subscription.EndTime.AddMonths(-subscription.RenewalPeriod))
                .FirstOrDefaultAsync();
            if (existingPayment != null)
            {
                return BadRequest(new { message = "Płatność dokonana" });
            }

            var activeDiscount = await _context.Discounts
                .Where(d => d.ClientId == paymentDto.IdClient && d.ValidFrom <= DateTime.UtcNow && d.ValidTo >= DateTime.UtcNow)
                .FirstOrDefaultAsync();

            decimal finalAmount = subscription.Price;
            if (activeDiscount != null && decimal.TryParse(activeDiscount.Value, out var discountValue))
            {
                finalAmount = subscription.Price - (subscription.Price * discountValue / 100);
            }

            if (paymentDto.Amount != finalAmount)
            {
                return BadRequest(new { message = "Kwota  nie jest zgodna" });
            }

            var payment = new Payment
            {
                IdClient = paymentDto.IdClient,
                IdSubscription = paymentDto.IdSubscription,
                Date = DateTime.UtcNow,
                Amount = paymentDto.Amount
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "OK", PaymentId = payment.Id });
        }
    }
}
