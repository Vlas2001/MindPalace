using System.Threading.Tasks;
using Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace MindPalace.Controllers
{
    [ApiController]
    public class AuthController: Controller
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            await _userService.RegisterUser(userDto);
            return Ok();
        }
        
        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            if (_userService.LoginUser(userLoginDto))
            {
                return Ok(true);
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Ok();
        }
    }
}