using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebApi.Models;

public class Booking
{
    [JsonProperty("id")]
    public string Id { get; set; }= Guid.NewGuid().ToString();

    public int EventId { get; set; }

    public int? EventPackageId { get; set; }

    public int CustomerId { get; set; }

    public string CustomerEmail { get; set; } = null!;

    public decimal AmountOfTickets { get; set; } = 1;

    public DateTime BookedAt { get; set; } = DateTime.UtcNow;
}
