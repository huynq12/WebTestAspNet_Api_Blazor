using WebTestApi.User;

namespace WebTestApi.Service
{
    public interface IUserService
    {
        public Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        public Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
    }

}
