using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FourthPro.Controllers.Website;

[Route("api/[controller]/[action]")]
[ApiController]
public class WebsiteUserController : ControllerBase
{
    public WebsiteUserController()
    {
    }
    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {

        return Ok();
    }
}
