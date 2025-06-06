using WebApi.Models;

namespace WebApi.Interfaces;

public interface IBookingService
{
    public Task<ServiceResult<Booking>> AddBookingAsync(BookingForm form, string email, string customerId);
    public Task<ServiceResult<Booking>> GetBookingAsync(int id);

    public Task<ServiceResult<IEnumerable<Booking>>> GetBookingsAsync();

    public Task<ServiceResult<IEnumerable<Booking>>> GetBookingsAsync(string customerId);

    public ServiceResult<int> CountBookingsByEventAsync(int eventId);

    public Task<ServiceResult<Booking>> UpdateBookingAsync(int id, BookingForm form, string email, string customerId);

    public Task<ServiceResult<bool>> DeleteBookingAsync(int id, string customerId);
}
