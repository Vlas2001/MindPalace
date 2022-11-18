using Dto;
using Dto.User;
using Microsoft.AspNetCore.Mvc;
using Service.Users;

namespace MindPalace.Controllers;

[ApiController]
[Route($"api/[controller]")]
public class UserController : BaseController
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("signUp")]
    public async Task<IActionResult> SignUp(SignUpModel signUpModel)
    {
        await _userService.SignUp(signUpModel);
        return Ok();
    }

    [HttpPost("signIn")]
    public async Task<IActionResult> SignIn(SignInModel signInModel)
    {
        return CreateActionResultFromData(await _userService.SignIn(signInModel));
    }
}