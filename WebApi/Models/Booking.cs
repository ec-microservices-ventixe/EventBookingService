

namespace WebApi.Models;

public class Booking
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int? EventPackageId { get; set; }

    public string CustomerId { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public int AmountOfTickets { get; set; } = 1;

    public decimal PriceToPay { get; set; }

    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
}
