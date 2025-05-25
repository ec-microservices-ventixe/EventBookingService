using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Data.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route(nameof(Controller))]
public class BookingController(IBookingRepository repository) : Controller
{
    private readonly IBookingRepository _repository = repository;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookingForm form)
    {
        try
        {
            var booking = new Booking { EventId = form.EventId, AmountOfTickets = form.AmountOfTickets, CustomerId = form.CustomerId, CustomerEmail = form.CustomerEmail, EventPackageId = form.EventPackageId };
            var res = await _repository.CreateAsync(booking);
            if(res == null) return StatusCode(500, "Internal Error");
            return Ok(res);

        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal Error");
        }    
    }
}
