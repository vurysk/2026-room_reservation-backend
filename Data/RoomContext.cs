using Microsoft.EntityFrameworkCore;
using  _2026_room_reservation_backend.Models;

namespace _2026_room_reservation_backend.Data;

public class RoomContext (DbContextOptions<RoomContext> options) : DbContext(options)
{
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // LOGIKA OTOMATIS GENERATE 24 RUANGAN
        var rooms = new List<Room>();
        int idCounter = 1;
        char[] blocks = { 'A', 'B', 'C', 'D' };

        foreach (var block in blocks)
        {
            for (int i = 101; i <= 106; i++)
            {
                rooms.Add(new Room 
                { 
                    Id = idCounter++, 
                    Code = $"{block}-{i}", 
                    Status = "available" 
                });
            }
        }
        
        // Daftarkan 24 ruangan ke database
        modelBuilder.Entity<Room>().HasData(rooms);

        // SEEDING DATA RESERVASI (Contoh Budi Tabuti agar Grid A-104 jadi Hijau nanti)
        modelBuilder.Entity<Reservation>().HasData(
            new Reservation 
            { 
                Id = 1, 
                FullName = "Budi Tabuti", 
                Nrp = "5025211000", 
                RoomCode = "A-104", 
                Purpose = "Rapat Koordinasi Projek", 
                Date = "2026-02-20", 
                Time = "10:00 - 12:00", 
                Status = "Approved", 
                SessionStatus = "Upcoming" 
            }
        );
    }
}
