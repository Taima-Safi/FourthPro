using FourthPro.Service.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourthPro.Controllers.Dashboard;

[Route("api/[controller]/[action]")]
[ApiController]
public class DashboardDepartmentController : ControllerBase
{
    private readonly IDepartmentService departmentService;

    public DashboardDepartmentController(IDepartmentService departmentService)
    {
        this.departmentService = departmentService;
    }
    [HttpPost, AllowAnonymous]
    public async Task<IActionResult> Add(string name)
    {
        var departmentId = await departmentService.AddAsync(name);
        return Ok(departmentId);
    }
    [HttpGet]
    public async Task<IActionResult> GatAll()
    {
        var result = await departmentService.GetAllAsync();
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatById(int departmentId)
    {
        var result = await departmentService.GetByIdAsync(departmentId);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GatDepartmentCount()
    {
        var result = await departmentService.GetDepartmentsCountAsync();
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int departmentId, string name)
    {
        await departmentService.UpdateAsync(departmentId, name);
        return Ok();
    }
    [HttpDelete]
    public async Task<IActionResult> Remove(int departmentId)
    {
        await departmentService.RemoveAsync(departmentId);
        return Ok();
    }
}
