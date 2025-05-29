using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Data.Context;
using WebApi.Data.Entities;
using WebApi.Data.Interfaces;

namespace WebApi.Data.Repositories;

public class BookingRepository(ApplicationDbContext context) : Repository<BookingEntity>(context), IBookingRepository
{
    private readonly ApplicationDbContext _context = context;
    public int CountBookings(int eventId)
    {
        return _context.Bookings.Where(x => x.EventId == eventId).Sum(x => x.AmountOfTickets);
    }
    public async Task<IEnumerable<BookingEntity>> FilterBookingsByEventId(int eventId)
    {
        return await _context.Bookings.Where(x => x.EventId == eventId).ToListAsync();
    }
    public async Task<IEnumerable<BookingEntity>> FilterBookingsByCustomerId(string customerId)
    {
        return await _context.Bookings.Where(x => x.CustomerId == customerId).ToListAsync();
    }
}
