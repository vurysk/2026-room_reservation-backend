using System;

namespace _2026_room_reservation_backend.Models;

public class Room
{
    public int Id { get; set; } // ID Utama (Primary Key)
    public required string Code { get; set; } // Contoh: A-101
    public required string Status { get; set; } // available, pending, approved
}
