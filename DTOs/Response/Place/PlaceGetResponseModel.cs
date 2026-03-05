namespace Bourt.DTOs.Response.Place
{
    public class PlaceGetPageModel
    {
        public int TotalData { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public List<PlaceGetResponseModel> Datas { get; set; } = new List<PlaceGetResponseModel>();
    }
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
