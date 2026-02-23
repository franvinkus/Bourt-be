namespace Bourt.DTOs.Request.Court
{
    public class CourtInsertRequestModel
    {
        public Guid PlaceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public decimal PricePerHour { get; set; }
    }
}
