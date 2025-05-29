using System.Diagnostics;
using WebApi.Data.Entities;
using WebApi.Data.Interfaces;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Services;

public class BookingService(IBookingRepository bookingRepository, IEventInfoRepository eventInfoRepository) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IEventInfoRepository _eventInfoRepository = eventInfoRepository;

    public async Task<ServiceResult<Booking>> AddBookingAsync(BookingForm form)
    {
        try
        {
            int totalTickets = (await _eventInfoRepository.Get(x => x.EventId ==  form.EventId)).TotalTickets;
            int totalBookings = _bookingRepository.CountBookings(form.EventId);
            int numOfTicketsRequested = form.AmountOfTickets;

            if((totalTickets - totalBookings) < numOfTicketsRequested)
            {
                return ServiceResult<Booking>.Conflict("There is not enough tickets left to fullfill this booking");
            }

            var entity = new BookingEntity { EventId = form.EventId, EventPackageId = form.EventPackageId, CustomerId = form.CustomerId, CustomerEmail = form.CustomerEmail, AmountOfTickets = form.AmountOfTickets, PriceToPay = form.PriceToPay};
            var createdEntity = await _bookingRepository.Create(entity);
            if (createdEntity is null) return ServiceResult<Booking>.Error("failed to book event");

            var booking = new Booking { Id = createdEntity.Id, EventId = createdEntity.EventId, EventPackageId = createdEntity.EventPackageId, CustomerId = createdEntity.CustomerId, CustomerEmail = createdEntity.CustomerEmail, AmountOfTickets = createdEntity.AmountOfTickets, PriceToPay = createdEntity.PriceToPay, BookedAt = createdEntity.BookedAt };
            return ServiceResult<Booking>.Ok(booking);

        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ServiceResult<Booking>.Error("failed to book event");
        }
    }

    public async Task<ServiceResult<bool>> DeleteBookingAsync(int id)
    {
        try
        {
            var entity = await _bookingRepository.Get(x => x.Id == id);
            if (entity is null) return ServiceResult<bool>.NotFound("Could not find booking");
            bool result = await _bookingRepository.Delete(entity);
            if (result == false) return ServiceResult<bool>.Error("failed to unbook event");
            return ServiceResult<bool>.NoContent();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ServiceResult<bool>.Error("failed to unbook event");
        }
    }

    public async Task<ServiceResult<Booking>> GetBookingAsync(int id)
    {
        try
        {
            var entity = await _bookingRepository.Get(x => x.Id == id);
            if (entity is null) return ServiceResult<Booking>.NotFound("Could not find booking");
            var booking = new Booking { Id = entity.Id, EventId = entity.EventId, EventPackageId = entity.EventPackageId, CustomerId = entity.CustomerId, CustomerEmail = entity.CustomerEmail, AmountOfTickets = entity.AmountOfTickets, PriceToPay = entity.PriceToPay, BookedAt = entity.BookedAt };
            return ServiceResult<Booking>.Ok(booking);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ServiceResult<Booking>.Error("failed to fetch booking");
        }
    }

    public async Task<ServiceResult<IEnumerable<Booking>>> GetBookingsAsync()
    {
        try
        {
            var entities = await _bookingRepository.GetAll();
            var bookings = entities.Select(x => new Booking { Id = x.Id, EventId = x.EventId, EventPackageId = x.EventPackageId, CustomerId = x.CustomerId, CustomerEmail = x.CustomerEmail, AmountOfTickets = x.AmountOfTickets, PriceToPay = x.PriceToPay, BookedAt = x.BookedAt }).ToList();
            return ServiceResult<IEnumerable<Booking>>.Ok(bookings);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ServiceResult<IEnumerable<Booking>>.Error("failed to fetch bookings");
        }
    }

    public async Task<ServiceResult<Booking>> UpdateBookingAsync(int id, BookingForm form)
    {
        try
        {
            var entity = await _bookingRepository.Get(x => x.Id == id);
            if (entity is null) return ServiceResult<Booking>.NotFound("Could not find booking");

            if(entity.AmountOfTickets < form.AmountOfTickets)
            {
                int totalTickets = (await _eventInfoRepository.Get(x => x.EventId == form.EventId)).TotalTickets;
                int totalBookings = _bookingRepository.CountBookings(form.EventId);
                int numOfTicketsRequested = form.AmountOfTickets;

                if ((totalTickets - totalBookings) < numOfTicketsRequested)
                {
                    return ServiceResult<Booking>.Conflict("There is not enough tickets left to fullfill this booking");
                }
            }

            entity.AmountOfTickets = form.AmountOfTickets;
            entity.PriceToPay = form.PriceToPay;
            entity.EventPackageId = form.EventPackageId;

            var updatedEntity = await _bookingRepository.Update(entity);
            if (updatedEntity is null) return ServiceResult<Booking>.Error("failed to update booking");

            var booking = new Booking { Id = updatedEntity.Id, EventId = updatedEntity.EventId, EventPackageId = updatedEntity.EventPackageId, CustomerId = updatedEntity.CustomerId, CustomerEmail = updatedEntity.CustomerEmail, AmountOfTickets = updatedEntity.AmountOfTickets, PriceToPay = updatedEntity.PriceToPay, BookedAt = updatedEntity.BookedAt };
            return ServiceResult<Booking>.Ok(booking);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ServiceResult<Booking>.Error("failed to update booking");
        }
    }
}
