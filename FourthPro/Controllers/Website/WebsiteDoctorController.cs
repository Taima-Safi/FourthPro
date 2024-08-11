using FourthPro.Service.Department;
using FourthPro.Service.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace FourthPro.Controllers.Website;

[Route("api/[controller]/[action]")]
[ApiController]
public class WebsiteDoctorController : ControllerBase
{
    private readonly IDoctorService doctorService;
    private readonly IDepartmentService departmentService;

    public WebsiteDoctorController(IDoctorService doctorService, IDepartmentService departmentService)
    {
        this.doctorService = doctorService;
        this.departmentService = departmentService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll(string departmentName, string doctorName)
    {
        var result = await doctorService.GetAllAsync(departmentName, doctorName);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatById(int doctorId)
    {
        var result = await doctorService.GetByIdAsync(doctorId);
        return Ok(result);
    }
    #region Department
    [HttpGet]
    public async Task<IActionResult> GetAllDepartment()
    {
        var result = await departmentService.GetAllAsync();
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetDepartmentById(int departmentId)
    {
        var result = await departmentService.GetByIdAsync(departmentId);
        return Ok(result);
    }
    #endregion
}
