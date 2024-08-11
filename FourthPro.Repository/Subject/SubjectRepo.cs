using FourthPro.Database.Context;
using FourthPro.Database.Model;
using FourthPro.Dto.Doctor;
using FourthPro.Dto.Lecture;
using FourthPro.Dto.Student;
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

    #region Subject
    public async Task<int> AddAsync(SubjectFormDto dto, string fileName)
    {
        var subject = await context.Subject.AddAsync(new SubjectModel
        {
            Year = dto.Year,
            Type = dto.Type,
            File = fileName,
            Title = dto.Title,
            DoctorId = dto.DoctorId,
            Semester = dto.Semester,
            IsDefault = dto.IsDefault,
            Description = dto.Description,
        });
        await context.SaveChangesAsync();
        return subject.Entity.Id;
    }
    public async Task<string> GetLastQuestionsFileNameById(int subjectId)
    => await context.Subject.Where(s => s.Id == subjectId).Select(s => s.File).FirstOrDefaultAsync();

    public async Task<List<SubjectDto>> GetAllAsync(YearType? year, SemesterType? semester, bool? isDefault, string title) // with filter for year
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

    public async Task<List<SubjectDto>> GatAllCurrentUserSubjectAsync(int userId, YearType? year) // with filter for year
        => await context.StudentSubject.Where(u => u.UserId == userId && (!year.HasValue || u.Subject.Year == year))
            .Select(s => new SubjectDto
            {
                Id = s.Subject.Id,
                File = s.Subject.File,
                Type = s.Subject.Type,
                Title = s.Subject.Title,
                Semester = s.Subject.Semester,
                Description = s.Subject.Description,
            }).ToListAsync();

    public async Task<bool> CheckIfExistAsync(int subjectId)
        => await context.Subject.Where(s => s.Id == subjectId).AnyAsync();

    public async Task<SubjectDto> GetByIdAsync(int subjectId)
        => await context.Subject.Where(s => s.Id == subjectId)
        .Select(s => new SubjectDto
        {
            Id = s.Id,
            Type = s.Type,
            Year = s.Year,
            File = s.File,
            Title = s.Title,
            Semester = s.Semester,
            IsDefault = s.IsDefault,
            Description = s.Description,
            Doctor = new DoctorDto
            {
                Id = s.Doctor.Id,
                Name = s.Doctor.Name,
                Email = s.Doctor.Email
            },
            Lectures = s.Lecture.Select(l => new LectureDto
            {
                Id = l.Id,
                File = l.File,
                Title = l.Title,
                IsPractical = l.IsPractical,
            }).ToList()
        }).FirstOrDefaultAsync();

    public async Task<int> GetSubjectCountAsync(YearType? year, SemesterType? semester)
        => await context.Subject.Where(s => (!year.HasValue || s.Year == year) && (!semester.HasValue || s.Semester == semester)).CountAsync();


    // TODO : SubjectUsers 
    public async Task<List<UserDto>> GetSubjectUsersAsync(int subjectId)
        => await context.StudentSubject.Where(p => p.Id == subjectId)
        .Select(p => new UserDto
        {
            Name = p.User.Name,
            Email = p.User.Email,
            Identifier = p.User.Identifier
        }).ToListAsync();

    public async Task<List<SubjectDto>> GetNonDefaultSubjectAsync(YearType year, SemesterType semester)
    => await context.Subject.Where(p => p.IsDefault == false && p.Year == year && p.Semester == semester)
    .Select(p => new SubjectDto
    {
        Id = p.Id,
        File = p.File,
        Type = p.Type,
        Title = p.Title,
        Description = p.Description,
        Doctor = new DoctorDto
        {
            Id = p.Doctor.Id,
            Name = p.Doctor.Name,
            Email = p.Doctor.Email,
        }
    }).ToListAsync();

    public async Task UpdateAsync(SubjectFormDto dto, int subjectId)
        => await context.Subject.Where(p => p.Id == subjectId).ExecuteUpdateAsync(p => p.SetProperty(p => p.Title, dto.Title).SetProperty(p => p.Type, dto.Type)
        .SetProperty(p => p.Description, dto.Description).SetProperty(p => p.Year, dto.Year).SetProperty(p => p.DoctorId, dto.DoctorId).SetProperty(p => p.Semester, dto.Semester)
        .SetProperty(p => p.IsDefault, dto.IsDefault));

    public async Task UpdateSubjectToAddFileAsync(string fileName, int subjectId)
    => await context.Subject.Where(s => s.Id == subjectId).ExecuteUpdateAsync(s => s.SetProperty(s => s.File, fileName));

    public async Task UpdateSubjectToRemoveFileAsync(int subjectId)
        => await context.Subject.Where(p => p.Id == subjectId).ExecuteUpdateAsync(p => p.SetProperty(p => p.File, ""));

    public async Task SelectFromOptionalSubjectsAsync(int subjectId, int userId)
    {
        await context.StudentSubject.AddAsync(new StudentSubjectModel
        {
            UserId = userId,
            SubjectId = subjectId,
        });
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int subjectId)
        => await context.Subject.Where(p => p.Id == subjectId).ExecuteDeleteAsync();
    #endregion


    #region Lecture
    public async Task<int> AddLectureAsync(LectureFormDto dto, string fileName)
    {
        var lecture = await context.Lecture.AddAsync(new LectureModel
        {
            File = fileName,
            Title = dto.Title,
            SubjectId = dto.SubjectId,
            IsPractical = dto.IsPractical
        });
        await context.SaveChangesAsync();
        return lecture.Entity.Id;
    }

    public async Task<bool> CheckIfLectureExistAsync(int lectureId)
        => await context.Lecture.Where(s => s.Id == lectureId).AnyAsync();
    public async Task<string> GetLectureFileNameById(int lectureId)
    => await context.Lecture.Where(s => s.Id == lectureId).Select(s => s.File).FirstOrDefaultAsync();
    public async Task UpdateLectureAsync(LectureFormDto dto, int lectureId)
        => await context.Lecture.Where(l => l.Id == lectureId).ExecuteUpdateAsync(l => l.SetProperty(l => l.Title, dto.Title).SetProperty(l => l.IsPractical, dto.IsPractical)
        .SetProperty(l => l.SubjectId, dto.SubjectId));

    public async Task UpdateLectureToAddFileAsync(string fileName, int lectureId)
    => await context.Lecture.Where(s => s.Id == lectureId).ExecuteUpdateAsync(s => s.SetProperty(s => s.File, fileName));

    public async Task UpdateLectureToRemoveFileAsync(int lectureId)
    => await context.Lecture.Where(p => p.Id == lectureId).ExecuteUpdateAsync(p => p.SetProperty(p => p.File, ""));

    public async Task<List<LectureDto>> GetAllLectureAsync(YearType? year, SemesterType? semester, bool? isPractice, int? subjectId, string title) // with filter for year
        => await context.Lecture.Where(s => (!subjectId.HasValue || s.SubjectId == subjectId) && (!year.HasValue || s.Subject.Year == year)
        && (!semester.HasValue || s.Subject.Semester == semester) && (!isPractice.HasValue || s.IsPractical == isPractice)
        && (string.IsNullOrEmpty(title) || s.Title.Contains(title)))
            .Select(s => new LectureDto
            {
                Id = s.Id,
                File = s.File,
                Title = s.Title,
                IsPractical = s.IsPractical,
                Subject = new SubjectDto
                {
                    Id = s.Subject.Id,
                    Year = s.Subject.Year,
                    Type = s.Subject.Type,
                    Title = s.Subject.Title,
                    Semester = s.Subject.Semester,
                    IsDefault = s.Subject.IsDefault,
                    Description = s.Subject.Description,
                }
            }).ToListAsync();

    public async Task<LectureDto> GetLectureByIdAsync(int lectureId)
        => await context.Lecture.Where(s => s.Id == lectureId)
        .Select(s => new LectureDto
        {
            Id = s.Id,
            File = s.File,
            Title = s.Title,
            IsPractical = s.IsPractical,
            Subject = new SubjectDto
            {
                Id = s.Subject.Id,
                Year = s.Subject.Year,
                Type = s.Subject.Type,
                Title = s.Subject.Title,
                Semester = s.Subject.Semester,
                IsDefault = s.Subject.IsDefault,
                Description = s.Subject.Description,
            }
        }).FirstOrDefaultAsync();

    public async Task RemoveLectureAsync(int lectureId)
        => await context.Lecture.Where(p => p.Id == lectureId).ExecuteDeleteAsync();
    #endregion
}
