namespace Bourt.DTOs.Response.Court
{
    public class CourtGetDetailsResponseModel
    {
        public string CourtName { get; set; } = string.Empty;
        public int CourtNumber { get; set; }
        public decimal CourtPricePerHour { get; set; }

    }
}
