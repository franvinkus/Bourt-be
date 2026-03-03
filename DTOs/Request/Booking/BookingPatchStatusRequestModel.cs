namespace Bourt.DTOs.Request.Booking
{
    public class BookingPatchStatusRequestModel
    {
        public Guid BookingId { get; set; }
        public string Status { get; set; }
    }
}
