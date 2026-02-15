using System;

namespace _2026_room_reservation_backend.Models;

public class Reservation
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Nrp { get; set; }
    public required string RoomCode { get; set; } // Jembatan ke Room.Code
    public required string Purpose { get; set; }
    public required string Date { get; set; }
    public required string Time { get; set; }
    public required string Status { get; set; } // Pending, Approved, Rejected
    public required string SessionStatus { get; set; } // Upcoming, On-Going, Completed, -
}
