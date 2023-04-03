using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTestApi.Data;
using WebTestApi.Service;
using WebTestApi.User;

namespace WebTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;

        public UserController(DataContext context,IUserService userService) {
            _context = context;   
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
                if (result.IsSuccess)
                {
                    return Ok(result);   
                }
                return BadRequest();
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest();
            }
            return BadRequest();
        }


    }
}
