namespace Bourt.DTOs.Request.Booking
{
    public class BookingInsertRequestModel
    {
        public Guid CourtId { get; set; }
        public Guid UserId { get; set; }
        public string Date {  get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
    }
}
