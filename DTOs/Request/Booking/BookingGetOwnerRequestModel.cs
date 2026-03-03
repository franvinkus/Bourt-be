namespace Bourt.DTOs.Request.Booking
{
    public class BookingGetOwnerRequestModel
    {
        public Guid PlaceId { get; set; }
        public Guid OwnerId { get; set; }
        public string? StringInput { get; set; } = string.Empty;
        public string? OrderDate { get; set; } = string.Empty;
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
