using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route(nameof(Controller))]
[Authorize]
public class BookingController(IBookingService bookingService) : Controller
{
    private readonly IBookingService _bookingService = bookingService;

    [HttpPost]
    public async Task<IActionResult> Create(BookingForm bookingForm)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userId is null || email is null) return Unauthorized();

            var result = await _bookingService.AddBookingAsync(bookingForm, email, userId);
            if(!result.Success) return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok(result.Data);
        } catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return StatusCode(500, "Internal Error");
        }
    }

    [HttpGet("/all-bookings")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllBookings()
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

    [HttpGet("/customers-bookings")]
    public async Task<IActionResult> GetCustomerBookings()
    {
        try
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return Unauthorized();

            var result = await _bookingService.GetBookingsAsync(userId);
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
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var result = await _bookingService.GetBookingAsync(id);
            if (!result.Success ) return StatusCode(result.StatusCode, result.ErrorMessage);
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userId is null || email is null) return Unauthorized();

            var result = await _bookingService.UpdateBookingAsync(id, bookingForm, email, userId);
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
