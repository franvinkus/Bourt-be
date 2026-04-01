namespace Bourt.DTOs.Response.Booking
{
    public class BookingGetOwnerPageModel
    {
        public int TotalData { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<BookingGetOwnerResponseModel> Datas { get; set; } = new List<BookingGetOwnerResponseModel>();
    }

    public class BookingGetOwnerResponseModel
    {
        public Guid BookingId { get; set; }
        public string PlaceName { get; set; } = string.Empty;
        public string CourtName { get; set; } = string.Empty;
        public int CourtNumber { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
