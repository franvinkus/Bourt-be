namespace Bourt.DTOs.Response.Place
{
    public class PlaceGetResponseModel
    {
        public Guid PlaceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string OpenHour { get; set; } = string.Empty;
        public string CloseHour { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
    }
}
