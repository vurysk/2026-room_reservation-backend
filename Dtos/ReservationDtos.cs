using System.ComponentModel.DataAnnotations;

namespace _2026_room_reservation_backend.Dtos;

public record RoomSummaryDto(string Id, string Code, string Status, string? ApplicantId);

public record CreateReservationDto(
    [Required(ErrorMessage = "Nama harus diisi")] 
    [StringLength(50, ErrorMessage = "Nama maksimal 50 karakter")] string FullName,
    [Required] 
    [RegularExpression(@"^\d+$", ErrorMessage = "NRP harus berupa angka")] string Nrp,
    [Required] string RoomCode,
    [Required] string Purpose,
    [Required] string Date, // Format: YYYY-MM-DD
    [Required] string Time // Format: HH:mm - HH:mm
);

public record UpdateReservationDto(
    [Required] [StringLength(50)] string FullName,
    [Required] [RegularExpression(@"^\d+$")] string Nrp,
    [Required] string Purpose,
    [Required] string Date,
    [Required] string Time
);

public record ReservationDetailDto(
    int Id, string FullName, string Nrp, string RoomCode, 
    string Purpose, string Date, string Time, 
    string Status, string SessionStatus
);

public record UpdateStatusDto([Required] string NewStatus);