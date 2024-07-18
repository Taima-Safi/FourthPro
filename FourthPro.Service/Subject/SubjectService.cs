using FourthPro.Dto.Subject;
using FourthPro.Repository.Subject;
using FourthPro.Service.Base;
using FourthPro.Shared.Enum;
using FourthPro.Shared.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FourthPro.Service.Subject;

public class SubjectService : BaseService, ISubjectService
{
    private readonly ISubjectRepo subjectRepo;

    public SubjectService(ISubjectRepo subjectRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        : base(configuration, httpContextAccessor)
    {
        this.subjectRepo = subjectRepo;
    }
    public async Task<int> AddAsync(SubjectFormDto dto)
    {
        return await subjectRepo.AddAsync(dto);
    }

    public async Task<List<SubjectDto>> GetAllAsync(YearType? year, SemesterType? semester, bool? isDefault, string? title)
    {
        return await subjectRepo.GetAllAsync(year, semester, isDefault, title);
    }
    public async Task<SubjectDto> GetByIdAsync(int subjectId)
    {
        return await subjectRepo.GetByIdAsync(subjectId) ??
            throw new NotFoundException("Subject not found..");
    }
    public async Task<int> GetSubjectCountAsync(YearType? year, SemesterType? semester)
    {
        return await subjectRepo.GetSubjectCountAsync(year, semester);
    }
    public async Task UpdateAsync(SubjectFormDto dto, int subjectId)
    {
        if (!await subjectRepo.CheckIfExistAsync(subjectId))
            throw new NotFoundException("Subject not found..");

        await subjectRepo.UpdateAsync(dto, subjectId);
    }
    public async Task RemoveAsync(int subjectId)
    {
        if (!await subjectRepo.CheckIfExistAsync(subjectId))
            throw new NotFoundException("Subject not found..");

        await subjectRepo.RemoveAsync(subjectId);
    }
}