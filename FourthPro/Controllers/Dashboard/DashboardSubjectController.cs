using FourthPro.Dto.Lecture;
using FourthPro.Dto.Subject;
using FourthPro.Service.Subject;
using FourthPro.Shared.Enum;
using FourthPro.Uploads;
using Microsoft.AspNetCore.Authorization;
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
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Add(SubjectFormDto dto)
    {
        var result = await subjectService.AddAsync(dto);
        return Ok(result);
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
    [HttpPost]
    public async Task<IActionResult> UpdateSubjectToAddFile(IFormFile file, int subjectId)
    {
        await subjectService.UpdateSubjectToAddFileAsync(file, subjectId);
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> UpdateSubjectToRemoveFile(int subjectId)
    {
        await subjectService.UpdateSubjectToRemoveFileAsync(subjectId);
        return Ok();
    }

    #region Lecture
    [HttpPost]
    public async Task<IActionResult> AddLecture(LectureFormDto dto)
    {
        var result = await subjectService.AddLectureAsync(dto);
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
    [HttpPost]
    public async Task<IActionResult> UpdateLecture(LectureFormDto dto, int lectureId)
    {
        await subjectService.UpdateLectureAsync(dto, lectureId);
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveLecture(int lectureId)
    {
        await subjectService.RemoveLectureAsync(lectureId);
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> UpdateLectureToAddFile(IFormFile file, int lectureId)
    {
        await subjectService.UpdateLectureToAddFileAsync(file, lectureId);
        return Ok();
    }
    [HttpPost]
    public async Task<IActionResult> UpdateLectureToRemoveFile(int lectureId)
    {
        await subjectService.UpdateLectureToRemoveFileAsync(lectureId);
        return Ok();
    }

    #endregion
}
