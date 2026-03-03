namespace Bourt.DTOs.Response.Booking
{
    public class BookinGetAllPagedResponseModel
    {
        public int TotalData { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<BookingGetAllResponseModel> GetAll { get; set; } = new List<BookingGetAllResponseModel>();
    }

    public class BookingGetAllResponseModel
    {
        public Guid BookingId { get; set; }
        public Guid PlaceId { get; set; }
        public string PlaceName { get; set; } = string.Empty;
        public Guid CourtId { get; set; }
        public int CourtNumber { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }

}
