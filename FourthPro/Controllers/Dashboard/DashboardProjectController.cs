using FourthPro.Dto.Project;
using FourthPro.Service.Project;
using FourthPro.Shared.Enum;
using Microsoft.AspNetCore.Mvc;

namespace FourthPro.Controllers.Dashboard;

[Route("api/[controller]/[action]")]
[ApiController]
public class DashboardProjectController : ControllerBase
{
    private readonly IProjectService projectService;

    public DashboardProjectController(IProjectService projectService)
    {
        this.projectService = projectService;
    }
    //[HttpPost, AllowAnonymous] this for website
    //public async Task<IActionResult> Add(ProjectFormDto dto)
    //=> Ok(await projectService.AddAsync(dto));
    [HttpGet]
    public async Task<IActionResult> GatAll(SectionType? type, DateTime? date)
    {
        var result = await projectService.GetAllAsync(type, date);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatById(int projectId)
    {
        var result = await projectService.GetByIdAsync(projectId);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetProjectCount(int? doctorId, SectionType? departmentType, string tool)
    {
        var result = await projectService.GetProjectCountAsync(doctorId, departmentType, tool);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Update(ProjectFormDto dto, int projectId)
    {
        await projectService.UpdateAsync(dto, projectId);
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> Remove(int projectId)
    {
        await projectService.RemoveAsync(projectId);
        return Ok();
    }
}
