using System.Text.Json.Serialization;

namespace Bourt.DTOs.Request.Booking
{
    public class BookingGetAvailableCourtHoursRequest
    {
        [JsonIgnore]
        public Guid CourtId { get; set; }
        public string Date { get; set; } = string.Empty;
    }
}
