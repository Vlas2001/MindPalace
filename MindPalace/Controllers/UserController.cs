using Dto;
using Dto.User;
using Microsoft.AspNetCore.Mvc;
using Service.Users;

namespace MindPalace.Controllers;

[ApiController]
[Route($"api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp(SignUpModel signUpModel)
    {
        await _userService.SignUp(signUpModel);
        return Ok();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn(SignInModel signInModel)
    {
        return Ok(await _userService.SignIn(signInModel));
    }
}