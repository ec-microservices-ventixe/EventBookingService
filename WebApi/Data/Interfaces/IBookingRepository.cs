using WebApi.Data.Entities;

namespace WebApi.Data.Interfaces;

public interface IBookingRepository : IRepository<BookingEntity>
{
    public int CountBookings(int eventId);

    public Task<IEnumerable<BookingEntity>> FilterBookingsByEventId(int eventId);
}
