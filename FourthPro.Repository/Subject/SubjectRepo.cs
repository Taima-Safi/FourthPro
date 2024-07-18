using FourthPro.Database.Context;
using FourthPro.Database.Model;
using FourthPro.Dto.Doctor;
using FourthPro.Dto.Subject;
using FourthPro.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace FourthPro.Repository.Subject;

public class SubjectRepo : ISubjectRepo
{
    private readonly FourthProDbContext context;

    public SubjectRepo(FourthProDbContext context)
    {
        this.context = context;
    }
    public async Task<int> AddAsync(SubjectFormDto dto)
    {
        var subject = await context.Subject.AddAsync(new SubjectModel
        {
            Year = dto.Year,
            Type = dto.Type,
            Title = dto.Title,
            DoctorId = dto.DoctorId,
            Semester = dto.Semester,
            IsDefault = dto.IsDefault,
            Description = dto.Description,
        });
        await context.SaveChangesAsync();
        return subject.Entity.Id;
    }

    public async Task<List<SubjectDto>> GetAllAsync(YearType? year, SemesterType? semester, bool? isDefault, string? title) // with filter for year
        => await context.Subject.Where(s => (!year.HasValue || s.Year == year) && (!semester.HasValue || s.Semester == semester)
        && (!isDefault.HasValue || s.IsDefault == isDefault) && (string.IsNullOrEmpty(title) || s.Title.Contains(title)))
            .Select(s => new SubjectDto
            {
                Id = s.Id,
                Type = s.Type,
                Title = s.Title,
                Semester = s.Semester,
                Year = s.Year,
                IsDefault = s.IsDefault,
                Description = s.Description,
                Doctor = new DoctorDto
                {
                    Id = s.Doctor.Id,
                    Name = s.Doctor.Name
                },
                File = s.File
            }).ToListAsync();

    public async Task<bool> CheckIfExistAsync(int subjectId)
        => await context.Subject.Where(s => s.Id == subjectId).AnyAsync();

    public async Task<SubjectDto> GetByIdAsync(int subjectId)
        => await context.Subject.Where(s => s.Id == subjectId)
        .Select(s => new SubjectDto
        {
            Id = s.Id,
            Type = s.Type,
            Title = s.Title,
            Semester = s.Semester,
            Year = s.Year,
            IsDefault = s.IsDefault,
            Description = s.Description,
            Doctor = new DoctorDto
            {
                Id = s.Doctor.Id,
                Name = s.Doctor.Name
            }
        }).FirstOrDefaultAsync();

    public async Task<int> GetSubjectCountAsync(YearType? year, SemesterType? semester)
        => await context.Subject.Where(s => (!year.HasValue || s.Year == year) && (!semester.HasValue || s.Semester == semester)).CountAsync();


    // SubjectUsers
    //public async Task<List<UserDto>> GetProjectUsersAsync(int projectId)
    //    => await context.Project.Where(p => p.Id == projectId)
    //    .Select(p => new UserDto
    //    {
    //        Name = p.Users.Select(u => u.Name).FirstOrDefault(),
    //        Email = p.Users.Select(u => u.Email).FirstOrDefault(),
    //        Identifier = p.Users.Select(u => u.Identifier).FirstOrDefault()
    //    }).ToListAsync();

    public async Task UpdateAsync(SubjectFormDto dto, int subjectId)
        => await context.Subject.Where(p => p.Id == subjectId).ExecuteUpdateAsync(p => p.SetProperty(p => p.Title, dto.Title).SetProperty(p => p.Type, dto.Type)
        .SetProperty(p => p.Description, dto.Description).SetProperty(p => p.Year, dto.Year).SetProperty(p => p.DoctorId, dto.DoctorId).SetProperty(p => p.Semester, dto.Semester)
        .SetProperty(p => p.IsDefault, dto.IsDefault));

    public async Task RemoveAsync(int subjectId)
        => await context.Subject.Where(p => p.Id == subjectId).ExecuteDeleteAsync();
}
