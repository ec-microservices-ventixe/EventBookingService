namespace WebApi.Models;

public class BookingForm
{
    public int EventId { get; set; }

    public int? EventPackageId { get; set; }

    public int CustomerId { get; set; }

    public string CustomerEmail { get; set; } = null!;

    public int AmountOfTickets { get; set; } = 1;

    public decimal PriceToPay { get; set; }
}
