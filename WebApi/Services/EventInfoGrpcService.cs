using Grpc.Core;
using System.Diagnostics;
using WebApi.Data.Entities;
using WebApi.Data.Interfaces;

namespace WebApi.Services;

public class EventInfoGrpcService(IEventInfoRepository eventInfoRepository, IBookingRepository bookingRepository) : EventInfoManager.EventInfoManagerBase
{
    private readonly IEventInfoRepository _eventInfoRepository = eventInfoRepository;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    public override async Task<EventInfoRes> UpdateEventInfo(EventInfoUpdateReq request, ServerCallContext context)
    {
        try
        {
            var entity = await _eventInfoRepository.Get(x => x.EventId == request.EventId);
            if (entity != null)
            {
                entity.TotalTickets = request.TotalTickets;
                var updatedEntity = await _eventInfoRepository.Update(entity);
                if (updatedEntity is null) return new EventInfoRes { Success = false, Message = "Failed to update event info" };
            }
            else
            {
                var newEntity = new EventInfoEntity { EventId = request.EventId, TotalTickets = request.TotalTickets };
                var created = await _eventInfoRepository.Create(newEntity);
                if (created is null) return new EventInfoRes { Success = false, Message = "Failed to create event info" };
            }
            return new EventInfoRes { Success = true, Message = "" };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new EventInfoRes { Success = false, Message = "Internal Error" };
        }
    }

    public override async Task<EventInfoRes> DeleteEventInfo(EventInfoDeleteReq request, ServerCallContext context)
    {
        try
        {
            var entity = await _eventInfoRepository.Get(x => x.EventId == request.EventId);
            if (entity != null)
            {
                bool deletedSuccessfully = await _eventInfoRepository.Delete(entity);
                if (!deletedSuccessfully) return new EventInfoRes { Success = false, Message = "Failed to delete event Info" };
            }
            var bookings = await _bookingRepository.FilterBookingsByEventId(request.EventId);
            foreach (var booking in bookings)
            {
                await _bookingRepository.Delete(booking);
            }
            return new EventInfoRes { Success = true, Message = "" };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new EventInfoRes { Success = false, Message = "Internal Error" };
        }
    }
}
