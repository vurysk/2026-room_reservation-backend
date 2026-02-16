using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_room_reservation_backend.Data;
using _2026_room_reservation_backend.Dtos;

namespace _2026_room_reservation_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(RoomContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomSummaryDto>>> GetRooms()
    {
        var rooms = await dbContext.Rooms.ToListAsync();
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        // Ambil reservasi hari ini yang belum batal/ditolak
        var todayReservations = await dbContext.Reservations
            .Where(r => r.Date == today && r.Status != "Cancelled" && r.Status != "Decline" && r.Status != "Rejected")
            .ToListAsync();

        var result = rooms.Select(room => {
            var res = todayReservations.FirstOrDefault(r => r.RoomCode == room.Code);
            
            string currentStatus = "available";

            if (res != null) {
                // CEK APAKAH SUDAH LEWAT JAMNYA (Agar kotak balik coklat)
                var session = CalculateSession(res.Date, res.Time);
                currentStatus = session == "Completed" ? "available" : res.Status.ToLower();
            }

            return new RoomSummaryDto(room.Id.ToString(), room.Code, currentStatus, res?.Id.ToString());
        });

        return Ok(result);
    }

    private string CalculateSession(string dateStr, string timeStr) {
        try {
            var endTime = timeStr.Split('-')[1].Trim();
            if (DateTime.Parse($"{dateStr} {endTime}") < DateTime.Now) return "Completed";
            return "Active";
        } catch { return "Active"; }
    }
}