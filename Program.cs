using Microsoft.EntityFrameworkCore;
using _2026_room_reservation_backend.Data;

var builder = WebApplication.CreateBuilder(args);

// Koneksi ke SQLite
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlite<RoomContext>(connectionString);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.MapGet("/", () => "API Room Reservation Ready!");

app.Run();