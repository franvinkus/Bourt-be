using Bourt.Enums;

namespace Bourt.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Enums.UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Place> Places { get; set; } = new List<Place>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
