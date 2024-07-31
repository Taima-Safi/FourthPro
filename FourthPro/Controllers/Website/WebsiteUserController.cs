using FourthPro.Dto.Student;
using FourthPro.Service.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourthPro.Controllers.Website;

[Route("api/[controller]/[action]")]
[ApiController]
public class WebsiteUserController : ControllerBase
{
    private readonly IUserService userService;

    public WebsiteUserController(IUserService userService)
    {
        this.userService = userService;
    }
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Add(UserFormDto dto)
    {
        var token = await userService.SignUpAsync(dto);
        return Ok(token);
    }
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> LogIn(int identifier, string password)
    {
        var token = await userService.SignInAsync(identifier, password);
        return Ok(token);
    }

    [HttpPut, AllowAnonymous]
    public new async Task<IActionResult> SignOut()
    {
        await userService.SignOutAsync();
        return Ok();
    }
    //[HttpGet, AllowAnonymous]
    //public async Task<IActionResult> GetAll()
    //    => Ok(await userService.GetAllAsync());
}
