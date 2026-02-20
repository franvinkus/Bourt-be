namespace Bourt.DTOs.Request
{
    public class UserChangeRoleRequestModel
    {
        public Guid Userid { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
