using Microsoft.EntityFrameworkCore;
using WebApi.Data.Entities;

namespace WebApi.Data.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<BookingEntity> Bookings { get; set; } = null!;
    public DbSet<EventInfoEntity> EventsInfo { get; set; } = null!;
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}
