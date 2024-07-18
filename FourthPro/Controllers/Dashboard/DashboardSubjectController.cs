using FourthPro.Dto.Subject;
using FourthPro.Service.Subject;
using FourthPro.Shared.Enum;
using Microsoft.AspNetCore.Mvc;

namespace FourthPro.Controllers.Dashboard;

[Route("api/[controller]/[action]")]
[ApiController]
public class DashboardSubjectController : ControllerBase
{
    private readonly ISubjectService subjectService;

    public DashboardSubjectController(ISubjectService subjectService)
    {
        this.subjectService = subjectService;
    }
    [HttpPost]
    public async Task<IActionResult> Add(SubjectFormDto dto)
    {
        var result = await subjectService.AddAsync(dto);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatAll(YearType? year, SemesterType? semester, bool? isDefault, string? title)
    {
        var result = await subjectService.GetAllAsync(year, semester, isDefault, title);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatById(int subjectId)
    {
        var result = await subjectService.GetByIdAsync(subjectId);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetSubjectCountAsync(YearType? year, SemesterType? semester)
    {
        var result = await subjectService.GetSubjectCountAsync(year, semester);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Update(SubjectFormDto dto, int subjectId)
    {
        await subjectService.UpdateAsync(dto, subjectId);
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> Remove(int subjectId)
    {
        await subjectService.RemoveAsync(subjectId);
        return Ok();
    }
}
