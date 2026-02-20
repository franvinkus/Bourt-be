using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Bourt.Data;
using Bourt.DTOs.Request;
using Bourt.DTOs.Response;
using Bourt.Enums;
using Bourt.Models;
using Bourt.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Bourt.Services.Interface
{
    public class UserServices: IUserServices
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;
        public UserServices(AppDbContext db, IConfiguration configuration) 
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<List<UserGetResponseModel>> Get(UserGetRequestModel request, CancellationToken cancellationToken)
        {
            var users = await _db.Users
                .Select(x => new UserGetResponseModel
                {
                    UserId = x.Id,
                    UserName = x.Username,
                    Email = x.Email,
                    Role = x.Role.ToString(),
                })
                .ToListAsync(cancellationToken);

            return users;
        }

        public async Task<UserRegisterResponseModel> Register (UserRegisterRequestModel request, CancellationToken cancellationToken)
        {
            var isUsernameExist = await _db.Users
                .Where(x => x.Username == request.Username || x.Email.ToLower() == request.Email.ToLower())
                .Select(x => new { x.Username, x.Email})
                .FirstOrDefaultAsync(cancellationToken);


            if (isUsernameExist != null)
            {
                if (isUsernameExist.Username.ToLower() == request.Username.ToLower())
                {
                    return new UserRegisterResponseModel { Message = "Username is Taken"};
                }
                else if (isUsernameExist.Email.ToLower() == request.Email.ToLower()) 
                {
                    return new UserRegisterResponseModel { Message = "Email is Taken" };
                }
            }
            else
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    CreatedAt = DateTime.UtcNow,
                };

                _db.Users.Add(newUser);
                await _db.SaveChangesAsync(cancellationToken);

            }

            return new UserRegisterResponseModel
            {
                Message = "Success"
            };
        }

        public async Task<UserLoginResponseModel> Login(UserLoginRequestModel request, CancellationToken cancellationToken)
        {
            var checkEmail = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
            bool checkPassword = BCrypt.Net.BCrypt.Verify(request.Password, checkEmail.PasswordHash);

            if (checkEmail == null || !checkPassword)
            {
                return new UserLoginResponseModel
                {
                    Message = "Email / Password is incorrect"
                };
            }
            else
            {
                var token = createToken(checkEmail);
                return new UserLoginResponseModel
                {
                    Message = "Success",
                    Token = token
                };
            }
        }

        public async Task<UserChangeRoleResponseModel> ChangeRole(UserChangeRoleRequestModel request, CancellationToken cancellationToken)
        {
            var checkUser = await _db.Users.FirstOrDefaultAsync(x => x.Id == request.Userid, cancellationToken);
            if(checkUser == null)
            {
                return new UserChangeRoleResponseModel
                {
                    Message = "User not found"
                };
            }

            var oldRole = checkUser.Role;

            if(!Enum.TryParse<UserRole>(request.RoleName, true, out var parsedRole))
            {
                return new UserChangeRoleResponseModel
                {
                    Message = "Invalid role name. input either (Admin, Owner, Customer)"
                };
            }

            checkUser.Role = parsedRole;
            await _db.SaveChangesAsync(cancellationToken);

            return new UserChangeRoleResponseModel
            {
                Message = $"Role has been change from {oldRole} to {parsedRole}"
            };
        }


        private string createToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
