namespace Bourt.DTOs.Request.Court
{
    public class CourtUpdateRequestModel
    {
        public Guid CourtId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public decimal PricePerHour { get; set; }
    }
}
