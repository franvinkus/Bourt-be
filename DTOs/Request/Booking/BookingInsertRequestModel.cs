using System.Text.Json.Serialization;

namespace Bourt.DTOs.Request.Booking
{
    public class BookingInsertRequestModel
    {
        [JsonIgnore]
        public Guid CourtId { get; set; }
        [JsonIgnore]

        public Guid UserId { get; set; }
        public string Date {  get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
    }
}
