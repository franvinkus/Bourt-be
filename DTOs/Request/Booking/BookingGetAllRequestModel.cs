namespace Bourt.DTOs.Request.Booking
{
    public class BookingGetAllRequestModel
    {
        public string? InputString{ get; set; } = string.Empty;
        public string? OrderDate { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set;} = 10;
    }
}
