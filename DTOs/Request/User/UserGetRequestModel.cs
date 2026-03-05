namespace Bourt.DTOs.Request.User
{
    public class UserGetRequestModel
    {
        public string? StringInput { get; set; } = string.Empty;
        public string? OrderState {  get; set; } = string.Empty;
        public int PageSize { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
    }
}
