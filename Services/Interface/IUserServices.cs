using Bourt.DTOs.Request.User;
using Bourt.DTOs.Response.User;

namespace Bourt.Services.Implementation
{
    public interface IUserServices
    {
        Task<List<UserGetResponseModel>> Get(UserGetRequestModel request, CancellationToken cancellationToken);
        Task<UserRegisterResponseModel> Register(UserRegisterRequestModel request, CancellationToken cancellationToken);

        Task<UserLoginResponseModel> Login(UserLoginRequestModel request, CancellationToken cancellationToken);

        Task<UserChangeRoleResponseModel> ChangeRole(UserChangeRoleRequestModel request, CancellationToken cancellationToken);
    }
}
