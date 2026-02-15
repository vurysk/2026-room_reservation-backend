using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _2026_room_reservation_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Nrp = table.Column<string>(type: "TEXT", nullable: false),
                    RoomCode = table.Column<string>(type: "TEXT", nullable: false),
                    Purpose = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    SessionStatus = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "Date", "FullName", "Nrp", "Purpose", "RoomCode", "SessionStatus", "Status", "Time" },
                values: new object[] { 1, "2026-02-20", "Budi Tabuti", "5025211000", "Rapat Koordinasi Projek", "A-104", "Upcoming", "Approved", "10:00 - 12:00" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Code", "Status" },
                values: new object[,]
                {
                    { 1, "A-101", "available" },
                    { 2, "A-102", "available" },
                    { 3, "A-103", "available" },
                    { 4, "A-104", "available" },
                    { 5, "A-105", "available" },
                    { 6, "A-106", "available" },
                    { 7, "B-101", "available" },
                    { 8, "B-102", "available" },
                    { 9, "B-103", "available" },
                    { 10, "B-104", "available" },
                    { 11, "B-105", "available" },
                    { 12, "B-106", "available" },
                    { 13, "C-101", "available" },
                    { 14, "C-102", "available" },
                    { 15, "C-103", "available" },
                    { 16, "C-104", "available" },
                    { 17, "C-105", "available" },
                    { 18, "C-106", "available" },
                    { 19, "D-101", "available" },
                    { 20, "D-102", "available" },
                    { 21, "D-103", "available" },
                    { 22, "D-104", "available" },
                    { 23, "D-105", "available" },
                    { 24, "D-106", "available" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
