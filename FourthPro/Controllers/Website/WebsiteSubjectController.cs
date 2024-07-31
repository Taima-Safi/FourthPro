using FourthPro.Service.Subject;
using FourthPro.Shared.Enum;
using FourthPro.Uploads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourthPro.Controllers.Website;

[Route("api/[controller]/[action]")]
[ApiController]
public class WebsiteSubjectController : ControllerBase
{
    private readonly ISubjectService subjectService;

    public WebsiteSubjectController(ISubjectService subjectService)
    {
        this.subjectService = subjectService;
    }
    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> DownloadLastQuestionFile(string fileName)
    {
        var result = await FileHelper.DownloadFile(fileName, false);
        return File(result, "application/octet-stream");
    }
    [HttpGet]
    public async Task<IActionResult> GetLastQuestionsFileNameById(int subjectId)
    {
        var result = await subjectService.GetLastQuestionsFileNameById(subjectId);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatAll(YearType? year, SemesterType? semester, bool? isDefault, string title)
    {
        var result = await subjectService.GetAllAsync(year, semester, isDefault, title);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> SelectFromOptionalSubjects(int subjectId)
    {
        await subjectService.SelectFromOptionalSubjectsAsync(subjectId);
        return Ok();
    }
    [HttpGet]
    public async Task<IActionResult> GatById(int subjectId)
    {
        var result = await subjectService.GetByIdAsync(subjectId);
        return Ok(result);
    }
    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> DownloadLectureFile(string fileName)
    {
        var result = await FileHelper.DownloadFile(fileName, true);
        return File(result, "application/octet-stream");
    }
    [HttpGet]
    public async Task<IActionResult> GetLectureFileNameById(int lectureId)
    {
        var result = await subjectService.GetLectureFileNameById(lectureId);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatAllLecture(YearType? year, SemesterType? semester, bool? isPractice, int? subjectId, string title)
    {
        var result = await subjectService.GetAllLectureAsync(year, semester, isPractice, subjectId, title);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatLectureById(int lectureId)
    {
        var result = await subjectService.GetLectureByIdAsync(lectureId);
        return Ok(result);
    }
}
