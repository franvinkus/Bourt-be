namespace Bourt.DTOs.Request.User
{
    public class UserChangeRoleRequestModel
    {
        public Guid Userid { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
