using WebTestApi.User;

namespace BlazorApiApp.Services
{
	public interface IAuthService
	{
		Task<UserManagerResponse> Login(LoginViewModel loginModel);
		Task Logout();
		
	}
}
