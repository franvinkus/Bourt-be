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
        public PagedCourts PagedCourts { get; set; } = new PagedCourts();
    }

    public class PagedCourts
    {
        public int TotalData { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<Courts> Datas { get; set; } = new List<Courts>();
    }

    public class Courts
    {
        public Guid CourtId { get; set; }
        public string CourtName { get; set; } = string.Empty;
        public int CourtNumber { get; set; }
        public decimal PricePerHour { get; set; }

    }
}
