using Microsoft.EntityFrameworkCore;
using _2026_room_reservation_backend.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Service Database
builder.Services.AddSqlite<RoomContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

// 2. Tambahkan CORS Policy (Agar React Bisa Akses)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000") // URL Vite atau CRA
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// 3. Gunakan CORS
app.UseCors("AllowReact");

app.MapControllers();
app.MapGet("/", () => "API Room Reservation Ready!!");

app.Run();