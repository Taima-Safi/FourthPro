using FourthPro.Database.Context;
using FourthPro.Database.Model;
using FourthPro.Dto.Department;
using FourthPro.Dto.Doctor;
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
    public async Task<List<DoctorDto>> GetAllAsync(string search)//filter by department name, can be null
        => await context.Doctor.Where(d => (string.IsNullOrEmpty(search) || d.Department.Name.Contains(search))).Select(d => new DoctorDto
        {
            Id = d.Id,
            Name = d.Name,
            Email = d.Email,
            Department = new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            }
        }).ToListAsync();

    public async Task<DoctorDto> GetById(int doctorId)
        => await context.Doctor.Select(d => new DoctorDto
        {
            Id = d.Id,
            Name = d.Name,
            Email = d.Email,
            Department = new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            }
        }).FirstOrDefaultAsync();

    public async Task<int> GetDoctorsCountAsync(string search)//filter by department name, can be null
    => await context.Doctor.Where(d => (string.IsNullOrEmpty(search) || d.Department.Name.Contains(search))).CountAsync();

    public async Task UpdateAsync(DoctorFormDto dto, int doctorId)
        => await context.Doctor.Where(d => d.Id == doctorId).ExecuteUpdateAsync(d => d.SetProperty(d => d.Email, dto.Email).SetProperty(d => d.DepartmentId, dto.DepartmentId).SetProperty(d => d.Name, dto.Name));

    public async Task RemoveAsync(int doctorId)
        => await context.Doctor.Where(d => d.Id == doctorId).ExecuteDeleteAsync();
    public async Task<bool> CheckIfExist(int doctorId)
        => await context.Doctor.Where(d => d.Id == doctorId).AnyAsync();
}
