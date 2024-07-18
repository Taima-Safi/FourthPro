using FourthPro.Dto.Subject;
using FourthPro.Shared.Enum;

namespace FourthPro.Repository.Subject;

public interface ISubjectRepo
{
    Task<int> AddAsync(SubjectFormDto dto);
    Task<bool> CheckIfExistAsync(int subjectId);
    Task<List<SubjectDto>> GetAllAsync(YearType? year, SemesterType? semester, bool? isDefault, string title);
    Task<SubjectDto> GetByIdAsync(int subjectId);
    Task<int> GetSubjectCountAsync(YearType? year, SemesterType? semester);
    Task RemoveAsync(int subjectId);
    Task UpdateAsync(SubjectFormDto dto, int subjectId);
}
