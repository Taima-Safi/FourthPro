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
    [HttpPost]
    public async Task<IActionResult> Add(StudentDto dto)
    {
        return Ok(await userService.AddAsync(dto));
    }
    [HttpGet, Authorize]
    public async Task<IActionResult> GetAll()
        => Ok(await userService.GetAllAsync());
}
