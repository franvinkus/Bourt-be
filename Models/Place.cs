namespace Bourt.Models
{
    public class Place
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid OwnerId { get; set; }
        public User OwnerName { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int SlotAvailibility { get; set; }
        public string OpenHour { get; set; } = string.Empty;
        public string CloseHour { get; set; } = string.Empty;
        public DateTime CreatedAt {  get; set; }
        public DateTime UpdateAt { get; set; }
        public ICollection<Court> Courts { get; set; } = new List<Court>();
    }
}
