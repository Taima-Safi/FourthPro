using FourthPro.Dto.Project;
using FourthPro.Service.Project;
using FourthPro.Shared.Enum;
using FourthPro.Uploads;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> DownloadProjectFile(string fileName)
    {
        var result = await FileHelper.DownloadFile(fileName, FileType.Project);
        return File(result, "application/octet-stream");
    }
    [HttpGet]
    public async Task<IActionResult> GetProjectFileNameById(int projectId)
    {
        var result = await projectService.GetProjectFileNameByIdAsync(projectId);
        return Ok(result);
    }
    [HttpGet]
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
    public async Task<IActionResult> GetAll(SectionType? type, DateTime? date)
    {
        var result = await projectService.GetAllAsync(type, date);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetUserProjects()
    {
        var result = await projectService.GetUserProjectsAsync();
        return Ok(result);
    }
}
