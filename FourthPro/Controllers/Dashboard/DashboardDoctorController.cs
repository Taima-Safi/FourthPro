using FourthPro.Dto.Doctor;
using FourthPro.Service.Doctor;
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

    [HttpPost]
    public async Task<IActionResult> Add(DoctorFormDto dto)
        => Ok(await doctorService.AddAsync(dto));
    [HttpGet]
    public async Task<IActionResult> GatAll(string search)
    {
        var result = await doctorService.GetAllAsync(search);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatById(int doctorId)
    {
        var result = await doctorService.GetByIdAsync(doctorId);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatDoctorCount(string search)
    {
        var result = await doctorService.GetDoctorsCountAsync(search);
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
