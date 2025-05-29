using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Data.Entities;

[Table("EventsInfo")]
public class EventInfoEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int EventId { get; set; }

    public int TotalTickets { get; set; }
}
