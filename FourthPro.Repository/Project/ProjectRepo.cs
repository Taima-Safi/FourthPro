using FourthPro.Database.Context;
using FourthPro.Database.Model;
using FourthPro.Dto.Doctor;
using FourthPro.Dto.Project;
using FourthPro.Dto.Student;
using Microsoft.EntityFrameworkCore;

namespace FourthPro.Repository.Project;

public class ProjectRepo : IProjectRepo
{
    private readonly FourthProDbContext context;

    public ProjectRepo(FourthProDbContext context)
    {
        this.context = context;
    }
    public async Task<int> AddAsync(ProjectFormDto dto)
    {
        var project = await context.Project.AddAsync(new ProjectModel
        {
            Type = dto.Type,
            Title = dto.Title,
            Tools = dto.Tools,
            DoctorId = dto.DoctorId,
            Description = dto.Description
        });
        await context.SaveChangesAsync();
        return project.Entity.Id;
    }

    public async Task<List<ProjectDto>> GetAllAsync(int? fourthProjectId, int? fifthProjectId) // with filter for year
        => await context.Project.Where(u => (!fourthProjectId.HasValue || u.Users.Select(u => u.FourthProjectId).FirstOrDefault() == fourthProjectId)
        && (!fifthProjectId.HasValue || u.Users.Select(u => u.FifthProjectId).FirstOrDefault() == fifthProjectId))
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Type = p.Type,
                Title = p.Title,
                Tools = p.Tools,
                Description = p.Description,
                Doctor = new DoctorDto
                {
                    Id = p.Doctor.Id,
                    Name = p.Doctor.Name
                }
            }).ToListAsync();

    public async Task<ProjectDto> GetProjectByIdAsync(int projectId)
        => await context.Project.Where(p => p.Id == projectId)
        .Select(p => new ProjectDto
        {
            Id = p.Id,
            Type = p.Type,
            Title = p.Title,
            Tools = p.Tools,
            Description = p.Description,
            Doctor = new DoctorDto
            {
                Id = p.Doctor.Id,
                Name = p.Doctor.Name
            },
            Users = p.Users.Select(u => new UserDto
            {
                Name = p.Users.Select(u => u.Name).FirstOrDefault(),
                Email = p.Users.Select(u => u.Email).FirstOrDefault(),
                Identifier = p.Users.Select(u => u.Identifier).FirstOrDefault()
            }).ToList()
        }).FirstOrDefaultAsync();

    public async Task<List<UserDto>> GetProjectUsersAsync(int projectId)
        => await context.Project.Where(p => p.Id == projectId)
        .Select(p => new UserDto
        {
            Name = p.Users.Select(u => u.Name).FirstOrDefault(),
            Email = p.Users.Select(u => u.Email).FirstOrDefault(),
            Identifier = p.Users.Select(u => u.Identifier).FirstOrDefault()
        }).ToListAsync();

    public async Task UpdateAsync(ProjectFormDto dto, int projectId)
        => await context.Project.Where(p => p.Id == projectId).ExecuteUpdateAsync(p => p.SetProperty(p => p.Title, dto.Title).SetProperty(p => p.Type, dto.Type)
        .SetProperty(p => p.Description, dto.Description).SetProperty(p => p.Tools, dto.Tools).SetProperty(p => p.DoctorId, dto.DoctorId));

    public async Task RemoveAsync(int projectId)
        => await context.Project.Where(p => p.Id == projectId).ExecuteDeleteAsync();
}
