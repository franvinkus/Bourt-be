namespace Bourt.DTOs.Response.Booking
{
    public class BookingInsertResponseModel
    {
        public Guid Id { get; set; }
        public Guid CourtId { get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
