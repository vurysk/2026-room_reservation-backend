using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_room_reservation_backend.Data;
using _2026_room_reservation_backend.Dtos;

namespace _2026_room_reservation_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController(RoomContext dbContext) : ControllerBase
{
    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<ReservationDetailDto>>> GetPending()
    {
        var data = await dbContext.Reservations.Where(r => r.Status == "Pending").ToListAsync();
        return Ok(data.Select(r => Map(r)));
    }

    [HttpPut("status/{roomCode}")]
    public async Task<IActionResult> UpdateStatus(string roomCode, [FromBody] UpdateStatusDto dto)
    {
        var res = await dbContext.Reservations
            .Where(r => r.RoomCode == roomCode && r.Status != "Cancelled")
            .OrderByDescending(r => r.Id).FirstOrDefaultAsync();

        if (res == null) return NotFound();
        res.Status = dto.NewStatus;
        await dbContext.SaveChangesAsync();
        return Ok(new { success = true });
    }

    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<ReservationDetailDto>>> GetHistory()
    {
        var data = await dbContext.Reservations.OrderByDescending(r => r.Id).ToListAsync();
        return Ok(data.Select(r => Map(r)));
    }

    private ReservationDetailDto Map(Models.Reservation r) {
        var times = r.Time.Split('-');
        var start = DateTime.Parse($"{r.Date} {times[0].Trim()}");
        var end = DateTime.Parse($"{r.Date} {times[1].Trim()}");
        string session = DateTime.Now < start ? "Upcoming" : (DateTime.Now <= end ? "On-Going" : "Completed");
        return new ReservationDetailDto(r.Id, r.FullName, r.Nrp, r.RoomCode, r.Purpose, r.Date, r.Time, r.Status, session);
    }
}