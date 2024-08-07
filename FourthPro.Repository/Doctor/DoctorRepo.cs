using FourthPro.Database.Context;
using FourthPro.Database.Model;
using FourthPro.Dto.Department;
using FourthPro.Dto.Doctor;
using FourthPro.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace FourthPro.Repository.Doctor;

public class DoctorRepo : IDoctorRepo
{
    private readonly FourthProDbContext context;

    public DoctorRepo(FourthProDbContext context)
    {
        this.context = context;
    }

    public async Task<int> AddAsync(DoctorFormDto dto)
    {
        var doctor = await context.Doctor.AddAsync(new DoctorModel
        {
            Name = dto.Name,
            Email = dto.Email,
            DepartmentId = dto.DepartmentId
        });
        await context.SaveChangesAsync();
        return doctor.Entity.Id;
    }
    public async Task<List<DoctorDto>> GetAllAsync(string departmentName, string doctorName)//filter by department name Or/and doctor name, can be null
        => await context.Doctor.Where(d => (string.IsNullOrEmpty(doctorName) || d.Name.Contains(doctorName))
        && (string.IsNullOrEmpty(departmentName) || d.Department.Title.Contains(departmentName)))
        .Select(d => new DoctorDto
        {
            Id = d.Id,
            Name = d.Name,
            Email = d.Email,
            Department = new DepartmentDto
            {
                Id = d.Department.Id,
                Title = d.Department.Title
            }
        }).ToListAsync();

    public async Task<DoctorDto> GetById(int doctorId)
        => await context.Doctor.Where(d => d.Id == doctorId).Select(d => new DoctorDto
        {
            Id = d.Id,
            Name = d.Name,
            Email = d.Email,
            Department = new DepartmentDto
            {
                Id = d.Department.Id,
                Title = d.Department.Title
            }
        }).FirstOrDefaultAsync();

    public async Task<int> GetDoctorsCountAsync(string search)//filter by department name, can be null
    => await context.Doctor.Where(d => (string.IsNullOrEmpty(search) || d.Department.Title.Contains(search))).CountAsync();

    public async Task<int> GetDoctorProjectCountAsync(SemesterType semester)
    {
        DateTime semesterDate = new();
        if (DateTime.UtcNow.Month > 11 && DateTime.UtcNow.Month < 2)//first semester
            semesterDate = DateTime.Parse($"11/1/{DateTime.UtcNow.Year}"); // month/day/year
        else
            semesterDate = DateTime.Parse($"11/1/{DateTime.UtcNow.Year - 1}");

        return await context.Project.Where(d => d.DoctorId == 1 && d.Date.Date > semesterDate && d.Date.Date < DateTime.UtcNow).CountAsync();
    }

    public async Task UpdateAsync(DoctorFormDto dto, int doctorId)
        => await context.Doctor.Where(d => d.Id == doctorId).ExecuteUpdateAsync(d => d.SetProperty(d => d.Email, dto.Email).SetProperty(d => d.DepartmentId, dto.DepartmentId).SetProperty(d => d.Name, dto.Name));

    public async Task RemoveAsync(int doctorId)
        => await context.Doctor.Where(d => d.Id == doctorId).ExecuteDeleteAsync();
    public async Task<bool> CheckIfExistAsync(int doctorId)
        => await context.Doctor.Where(d => d.Id == doctorId).AnyAsync();
}
