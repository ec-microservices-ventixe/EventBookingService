using WebApi.Data.Interfaces;
using WebApi.Models;

namespace WebApi.Data.Repositories;

public class BookingRepository(IConfiguration config) : Repository<Booking>(config), IBookingRepository
{
}
