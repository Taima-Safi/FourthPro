using FourthPro.Database.Context;
using FourthPro.Database.Model;
using FourthPro.Dto.Department;
using FourthPro.Dto.Doctor;
using Microsoft.EntityFrameworkCore;

namespace FourthPro.Repository.Department;

public class DepartmentRepo : IDepartmentRepo
{
    private readonly FourthProDbContext context;
    public DepartmentRepo(FourthProDbContext context)
    {
        this.context = context;
    }
    public async Task<int> AddAsync(string name)
    {
        var department = await context.AddAsync(new DepartmentModel { Title = name });

        await context.SaveChangesAsync();
        return department.Entity.Id;
    }

    public async Task<DepartmentDto> GetByIdAsync(int departmentId)
        => await context.Department.Where(d => d.Id == departmentId)
        .Select(d => new DepartmentDto
        {
            Id = d.Id,
            Title = d.Title,
            Doctors = d.Doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                Name = d.Name,
                Email = d.Email
            }).ToList()
        }).FirstOrDefaultAsync();

    public async Task<List<DepartmentDto>> GetAllAsync()
        => await context.Department.Select(d => new DepartmentDto
        {
            Id = d.Id,
            Title = d.Title,
            Doctors = d.Doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                Name = d.Name,
                Email = d.Email
            }).ToList()
        }).ToListAsync();

    public async Task<int> GetDepartmentsCountAsync()
        => await context.Department.CountAsync();

    public async Task UpdateAsync(int departmentId, string name)
        => await context.Department.Where(d => d.Id == departmentId).ExecuteUpdateAsync(d => d.SetProperty(d => d.Title, name));

    public async Task RemoveAsync(int departmentId)
        => await context.Department.Where(d => d.Id == departmentId).ExecuteDeleteAsync();

    public async Task<bool> CheckIfExist(int departmentId)
    => await context.Department.Where(d => d.Id == departmentId).AnyAsync();
}
