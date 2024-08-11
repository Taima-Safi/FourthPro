using FourthPro.Dto.Doctor;
using FourthPro.Service.Department;
using FourthPro.Service.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourthPro.Controllers.Dashboard;

[Route("api/[controller]/[action]")]
[ApiController]
public class DashboardDoctorController : ControllerBase
{
    private readonly IDoctorService doctorService;

    public DashboardDoctorController(IDoctorService doctorService)
    {
        this.doctorService = doctorService;
    }

    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Add(DoctorFormDto dto)
        => Ok(await doctorService.AddAsync(dto));
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
    [HttpGet]
    public async Task<IActionResult> GatDoctorCount(string departmentName)
    {
        var result = await doctorService.GetDoctorsCountAsync(departmentName);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Update(DoctorFormDto dto, int doctorId)
    {
        await doctorService.UpdateAsync(dto, doctorId);
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> Remove(int doctorId)
    {
        await doctorService.RemoveAsync(doctorId);
        return Ok();
    }
}
