namespace Bourt.DTOs.Response.Booking
{
    public class BookingGetAvailableCourtHoursResponse
    {
        public string Time { get; set; } = string.Empty;
        public bool IsBooked { get; set; }
    }
}
