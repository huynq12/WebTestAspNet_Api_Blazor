using Microsoft.AspNetCore.Identity;
using WebTestApi.User;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace WebTestApi.Service
{
    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public UserService(UserManager<IdentityUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new UserManagerResponse
                {
                    Message = "User is not exist!",
                    IsSuccess = false
                };

            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPassword)
                return new UserManagerResponse
                {
                    Message = "Password is not correct",
                    IsSuccess = false
                };

            var claims = new[]
            {
                new Claim("Email",model.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                claims:claims,
                expires : DateTime.Now.AddDays(1),
                signingCredentials:new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
            );


            return new UserManagerResponse
            {
                Message = "Success",
                IsSuccess = true,
                Expired = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
			};
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if(model == null)
            {
                throw new NullReferenceException("Register is not exist!");
            }
            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };
            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if(result.Succeeded)
                return new UserManagerResponse
                {
                    Message = "Register Successfully",
                    IsSuccess = true
                };
            return new UserManagerResponse
            {
                Message = "Cannot create new user",
                IsSuccess = false,
            };
        }
    }
}
