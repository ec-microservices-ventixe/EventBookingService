using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route(nameof(Controller))]
public class BookingController(IBookingService bookingService) : Controller
{
    private readonly IBookingService _bookingService = bookingService;

    [HttpPost]
    public async Task<IActionResult> Create(BookingForm bookingForm)
    {
        try
        {
            var result = await _bookingService.AddBookingAsync(bookingForm);
            if(!result.Success) return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok(result.Data);
        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        try
        {
            var result = await _bookingService.GetBookingsAsync();
            if (!result.Success) return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal Error");
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Create(int id)
    {
        try
        {
            var result = await _bookingService.GetBookingAsync(id);
            if (!result.Success) return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal Error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookingForm bookingForm)
    {
        try
        {
            var result = await _bookingService.UpdateBookingAsync(id, bookingForm);
            if (!result.Success) return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok(result.Data);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal Error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _bookingService.DeleteBookingAsync(id);
            if (!result.Success) return StatusCode(result.StatusCode, result.ErrorMessage);
            return NoContent();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal Error");
        }
    }
}
