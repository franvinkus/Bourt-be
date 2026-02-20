namespace Bourt.DTOs.Request
{
    public class PlaceInsertRequestModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string OpenHour { get; set; } = string.Empty;
        public string CloseHour {  get; set; } = string.Empty;

    }
}
