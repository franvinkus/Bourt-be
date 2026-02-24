namespace Bourt.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Court Court { get; set; }
        public Guid CourtId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Enums.BookingStatus Status { get; set; }
    }
}
