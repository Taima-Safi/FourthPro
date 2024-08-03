using FourthPro.Dto.Project;
using FourthPro.Service.Project;
using Microsoft.AspNetCore.Mvc;

namespace FourthPro.Controllers.Website;

[Route("api/[controller]/[action]")]
[ApiController]
public class WebsiteProjectController : ControllerBase
{
    private readonly IProjectService projectService;

    public WebsiteProjectController(IProjectService projectService)
    {
        this.projectService = projectService;
    }
    [HttpPost]
    public async Task<IActionResult> Add(ProjectFormDto dto)
    => Ok(await projectService.AddAsync(dto));
    public async Task<IActionResult> GatById(int projectId)
    {
        var result = await projectService.GetByIdAsync(projectId);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Update(ProjectFormDto dto, int projectId)
    {
        await projectService.UpdateAsync(dto, projectId);
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> GatAll(int? fourthProjectId, int? fifthProjectId)
    {
        var result = await projectService.GetAllAsync(fourthProjectId, fifthProjectId);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetUserProjects()
    {
        var result = await projectService.GetUserProjectsAsync();
        return Ok(result);
    }
}
