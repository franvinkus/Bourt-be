namespace Bourt.Models
{
    public class Court
    {
        public Guid Id { get; set; }
        public Guid PlaceId { get; set; }
        public Place Place { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public decimal PricePerHour { get; set; }
        public DateTime CreatedAt {  get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
