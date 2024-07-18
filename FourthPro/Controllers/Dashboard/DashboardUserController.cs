using FourthPro.Service.UserService;
using FourthPro.Shared.Enum;
using Microsoft.AspNetCore.Mvc;

namespace FourthPro.Controllers.Dashboard;

[Route("api/[controller]/[action]")]
[ApiController]
public class DashboardUserController : ControllerBase
{
    private readonly IUserService userService;

    public DashboardUserController(IUserService userService)
    {
        this.userService = userService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    => Ok(await userService.GetAllAsync());

    [HttpGet]
    public async Task<IActionResult> GetUserByIdentifier(int identifier)
    => Ok(await userService.GetUserByIdentifierAsync(identifier));

    //[HttpGet]
    //public async Task<IActionResult> GetUsersByProjectId(int projectId)
    //=> Ok(await userService.GetUsersByProjectIdAsync(projectId));

    [HttpGet]
    public async Task<IActionResult> GetUsersCount(YearType? year)
    => Ok(await userService.GetUsersCountAsync(year));
}
