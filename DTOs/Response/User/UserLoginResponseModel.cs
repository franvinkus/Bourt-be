namespace Bourt.DTOs.Response.User
{
    public class UserLoginResponseModel
    {
        public string Message { get; set; } = string.Empty;
        public string? Token { get; set; }
    }
}
