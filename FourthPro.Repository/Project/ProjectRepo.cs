using FourthPro.Database.Context;
using FourthPro.Database.Model;
using FourthPro.Dto.Doctor;
using FourthPro.Dto.Project;
using FourthPro.Dto.Student;
using FourthPro.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace FourthPro.Repository.Project;

public class ProjectRepo : IProjectRepo
{
    private readonly FourthProDbContext context;

    public ProjectRepo(FourthProDbContext context)
    {
        this.context = context;
    }
    public async Task<int> AddAsync(ProjectFormDto dto, string fileName)
    {
        var project = await context.Project.AddAsync(new ProjectModel
        {
            Url = dto.Url,
            File = fileName,
            Type = dto.Type,
            Title = dto.Title,
            Tools = dto.Tools,
            Date = DateTime.UtcNow,
            DoctorId = dto.DoctorId,
            Semester = dto.Semester,
            Description = dto.Description
        });
        await context.SaveChangesAsync();
        return project.Entity.Id;
    }
    public async Task UpdateUserProjectAsync(int projectId, SemesterType semester, int userId)
    {
        if (semester == SemesterType.First)
            await context.User.Where(u => u.Id == userId).ExecuteUpdateAsync(u => u.SetProperty(u => u.FifthProjectId, projectId));
        else
            await context.User.Where(u => u.Id == userId).ExecuteUpdateAsync(u => u.SetProperty(u => u.FourthProjectId, projectId));
    }

    public async Task<List<ProjectDto>> GetAllAsync(SectionType? type, DateTime? date)
        => await context.Project.Where(u => (!type.HasValue || u.Type == type) && (!date.HasValue || u.Date.Date == date))
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Url = p.Url,
                Type = p.Type,
                Date = p.Date,
                File = p.File,
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
                    Id = u.Id,
                    Name = u.Name,
                    Identifier = u.Identifier
                }).ToList()
            }).ToListAsync();

    public async Task<List<ProjectDto>> GetUserProjectsAsync(int userId)
        => await context.Project.Where(u => u.Users.Any(u => u.Id == userId))
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Url = p.Url,
                Type = p.Type,
                File = p.File,
                Date = p.Date,
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
                    Id = u.Id,
                    Name = u.Name,
                    Identifier = u.Identifier
                }).ToList()
            }).ToListAsync();

    public async Task<bool> CheckIfExistAsync(int projectId)
    => await context.Project.Where(s => s.Id == projectId).AnyAsync();

    public async Task<string> GetProjectFileNameByIdAsync(int projectId)
    => await context.Project.Where(s => s.Id == projectId).Select(s => s.File).FirstOrDefaultAsync();

    public async Task<ProjectDto> GetProjectByIdAsync(int projectId)
    => await context.Project.Where(p => p.Id == projectId)
        .Select(p => new ProjectDto
        {
            Id = p.Id,
            Url = p.Url,
            Type = p.Type,
            File = p.File,
            Date = p.Date,
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

    //TODO
    //public async Task<List<UserDto>> GetProjectUsersAsync(int projectId)
    //    => await context.Project.Where(p => p.Id == projectId)
    //    .Select(p => new UserDto
    //    {
    //        Name = p.Users.Select(u => u.Name).FirstOrDefault(),
    //        Email = p.Users.Select(u => u.Email).FirstOrDefault(),
    //        Identifier = p.Users.Select(u => u.Identifier).FirstOrDefault()
    //    }).ToListAsync();

    public async Task<int> GetProjectCountAsync(int? doctorId, SectionType? type, string tool)
    {
        return await context.Project.Where(p => (!doctorId.HasValue || p.DoctorId == doctorId) // TODO : when student add project for doctor check Count
        && (!type.HasValue || p.Type == type) && (string.IsNullOrEmpty(tool) || p.Tools.Contains(tool))).CountAsync();
    }

    public async Task UpdateAsync(ProjectFormDto dto, int projectId)
        => await context.Project.Where(p => p.Id == projectId).ExecuteUpdateAsync(p => p.SetProperty(p => p.Title, dto.Title).SetProperty(p => p.Type, dto.Type)
        .SetProperty(p => p.Description, dto.Description).SetProperty(p => p.Tools, dto.Tools).SetProperty(p => p.DoctorId, dto.DoctorId));

    public async Task RemoveAsync(int projectId)
        => await context.Project.Where(p => p.Id == projectId).ExecuteDeleteAsync();
}
