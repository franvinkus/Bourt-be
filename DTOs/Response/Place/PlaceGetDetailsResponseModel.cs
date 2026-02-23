namespace Bourt.DTOs.Response.Place
{
    public class PlaceGetDetailsResponseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string OpenHour { get; set; } = string.Empty;
        public string CloseHour { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public List<Courts> Courts { get; set; } = new List<Courts>();
    }

    public class Courts
    {
        public Guid CourtId { get; set; }
        public string CourtName { get; set; } = string.Empty;
        public int CourtNumber { get; set; }
        public decimal PricePerHour { get; set; }

    }
}
