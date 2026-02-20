namespace Bourt.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid CoursDetailsId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Enums.BookingStatus Status { get; set; }
    }
}
