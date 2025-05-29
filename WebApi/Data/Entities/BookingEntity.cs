using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data.Entities;

[Table("Bookings")]
public class BookingEntity
{
    [Key]
    public int Id { get; set; }

    public int EventId { get; set; }

    public int? EventPackageId { get; set; }

    public int CustomerId { get; set; }

    [Column(TypeName = "varchar(250)")]
    public string CustomerEmail { get; set; } = null!;

    public int AmountOfTickets { get; set; } = 1;

    [Column(TypeName = "money")]
    public decimal PriceToPay { get; set; }

    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
}
