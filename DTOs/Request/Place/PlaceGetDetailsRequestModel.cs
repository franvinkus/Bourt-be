namespace Bourt.DTOs.Request.Place
{
    public class PlaceGetDetailsRequestModel
    {
        public Guid PlaceId { get; set; }
        public string? StringInput { get; set; } = string.Empty;
        public string? OrderState { get; set; } = string.Empty;
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
