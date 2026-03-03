namespace Bourt.DTOs.Response.Booking
{
    public class BookingGetCustomerPageModel
    {
        public int TotalData { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<BookingGetCustomerResponseModel> Datas { get; set; } = new List<BookingGetCustomerResponseModel>();
    }

    public class BookingGetCustomerResponseModel
    {
        public Guid BookingId { get; set; }
        public string PlaceName { get; set; } = string.Empty;
        public int CourtNumber { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
