namespace Bourt.DTOs.Response.Booking
{
    public class BookinGetAllPagedResponseModel
    {
        public int TotalData { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<BookingGetAllResponseModel> datas { get; set; } = new List<BookingGetAllResponseModel>();
    }

    public class BookingGetAllResponseModel
    {
        public Guid BookingId { get; set; }
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; } = string.Empty;
        public Guid CourtId { get; set; }
        public string CourtName { get; set; } = string.Empty;
        public int CourtNumber { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }

}
