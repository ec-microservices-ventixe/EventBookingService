using WebApi.Models;

namespace WebApi.Interfaces;

public interface IBookingService
{
    public Task<ServiceResult<Booking>> AddBookingAsync(BookingForm form);
    public Task<ServiceResult<Booking>> GetBookingAsync(int id);

    public Task<ServiceResult<IEnumerable<Booking>>> GetBookingsAsync();

    public Task<ServiceResult<Booking>> UpdateBookingAsync(int id, BookingForm form);

    public Task<ServiceResult<bool>> DeleteBookingAsync(int id);
}
