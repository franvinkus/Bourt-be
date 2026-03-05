namespace Bourt.DTOs.Response.User
{
    public class UserGetPageModel
    {
        public int TotalData { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<UserGetResponseModel> Data { get; set; } = new List<UserGetResponseModel>();
    }

    public class UserGetResponseModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
