namespace WebApi.Models;

public class BookingForm
{
    public int EventId { get; set; }

    public int? EventPackageId { get; set; }

    public int CustomerId { get; set; }

    public string CustomerEmail { get; set; } = null!;

    public decimal AmountOfTickets { get; set; } = 1;

}
