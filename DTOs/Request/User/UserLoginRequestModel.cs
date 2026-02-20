namespace Bourt.DTOs.Request.User
{
    public class UserLoginRequestModel
    {
        public string Email {  get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
