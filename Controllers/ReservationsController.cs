using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _2026_room_reservation_backend.Data;
using _2026_room_reservation_backend.Models;
using _2026_room_reservation_backend.Dtos;

namespace _2026_room_reservation_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController(RoomContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDetailDto>>> GetAll() => 
        Ok((await dbContext.Reservations.OrderByDescending(r => r.Id).ToListAsync())
        .Select(r => MapDto(r)));

    [HttpGet("room/{roomCode}")]
    public async Task<ActionResult<ReservationDetailDto>> GetByRoom(string roomCode)
    {
        var res = await dbContext.Reservations
            .Where(r => r.RoomCode == roomCode && r.Status != "Cancelled" && r.Status != "Decline")
            .OrderByDescending(r => r.Id).FirstOrDefaultAsync();
        return res == null ? NotFound() : Ok(MapDto(res));
    }

    [HttpGet("my/{nrp}")]
    public async Task<ActionResult<IEnumerable<ReservationDetailDto>>> GetByNrp(string nrp) => 
        Ok((await dbContext.Reservations.Where(r => r.Nrp == nrp).OrderByDescending(r => r.Id).ToListAsync())
        .Select(r => MapDto(r)));

    [HttpPost]
    public async Task<IActionResult> Create(CreateReservationDto dto)
    {
        // VALIDASI: 1 Ruang 1 Hari & Tahun 2026 & Bukan Masa Lalu
        if (IsPast(dto.Date, dto.Time) || !dto.Date.StartsWith("2026"))
            return BadRequest(new { message = "Input tidak valid (Reservasi ruang hanya untuk tahun berjalan dan jam di masa depan)." });

        var exists = await dbContext.Reservations.AnyAsync(r => r.RoomCode == dto.RoomCode && r.Date == dto.Date && r.Status != "Cancelled" && r.Status != "Decline");
        if (exists) return BadRequest(new { message = "Ruangan sudah dipesan untuk tanggal ini." });

        var res = new Reservation { FullName = dto.FullName, Nrp = dto.Nrp, RoomCode = dto.RoomCode, Purpose = dto.Purpose, Date = dto.Date, Time = dto.Time, Status = "Pending", SessionStatus = "Upcoming" };
        dbContext.Reservations.Add(res);
        await dbContext.SaveChangesAsync();
        return Ok(new { success = true, id = res.Id });
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateReservationDto dto)
    {
        var res = await dbContext.Reservations.FindAsync(id);
        if (res == null) return NotFound();

        // 1. Validasi Status: Jika sudah Approved, tidak boleh diedit (Sama seperti Delete)
        if (res.Status == "Approved")
            return BadRequest(new { message = "Gagal. Data sudah disetujui Admin dan tidak bisa diubah lagi." });

        // 2. Validasi Waktu: Jangan sampai diedit ke waktu masa lalu atau tahun yang salah
        if (IsPast(dto.Date, dto.Time) || !dto.Date.StartsWith("2026"))
            return BadRequest(new { message = "Gagal. Tanggal atau waktu tidak valid." });

        // 3. Validasi Double Booking: Cek apakah jadwal baru sudah dipakai orang lain
        // Kita pakai r.Id != id supaya tidak bentrok dengan data diri sendiri yang sedang diedit
        var exists = await dbContext.Reservations.AnyAsync(r => 
            r.Id != id && 
            r.RoomCode == res.RoomCode && 
            r.Date == dto.Date && 
            r.Status != "Cancelled" && 
            r.Status != "Decline");

        if (exists) 
            return BadRequest(new { message = "Gagal. Ruangan sudah dipesan untuk tanggal tersebut." });

        // 4. Update data lama dengan data baru dari DTO
        res.FullName = dto.FullName;
        res.Nrp = dto.Nrp;
        res.Purpose = dto.Purpose;
        res.Date = dto.Date;
        res.Time = dto.Time;

        await dbContext.SaveChangesAsync();
        return Ok(new { success = true });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await dbContext.Reservations.FindAsync(id);
        if (res == null) return NotFound();
        if (res.Status == "Approved") return BadRequest(new { message = "Gagal. Sudah disetujui Admin." });
        res.Status = "Cancelled";
        await dbContext.SaveChangesAsync();
        return Ok(new { success = true });
    }

    // HELPERS
    private ReservationDetailDto MapDto(Reservation r) => 
        new(r.Id, r.FullName, r.Nrp, r.RoomCode, r.Purpose, r.Date, r.Time, r.Status, CalculateStatus(r.Date, r.Time));

    private string CalculateStatus(string d, string t) {
        try {
            var times = t.Split('-');
            var start = DateTime.Parse($"{d} {times[0].Trim()}");
            var end = DateTime.Parse($"{d} {times[1].Trim()}");
            if (DateTime.Now < start) return "Upcoming";
            return DateTime.Now <= end ? "On-Going" : "Completed";
        } catch { return "-"; }
    }

    private bool IsPast(string d, string t) {
        try { return DateTime.Parse($"{d} {t.Split('-')[0].Trim()}") < DateTime.Now; }
        catch { return false; }
    }
}