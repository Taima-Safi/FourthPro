using FourthPro.Dto.Lecture;
using FourthPro.Dto.Subject;
using FourthPro.Shared.Enum;
using Microsoft.AspNetCore.Http;

namespace FourthPro.Service.Subject;

public interface ISubjectService
{
    Task<int> AddAsync(SubjectFormDto dto);
    Task<List<SubjectDto>> GetAllAsync(YearType? year, SemesterType? semester, bool? isDefault, string title);
    Task<SubjectDto> GetByIdAsync(int subjectId);
    Task<string> GetLastQuestionsFileNameById(int subjectId);
    Task<int> GetSubjectCountAsync(YearType? year, SemesterType? semester);
    Task RemoveAsync(int subjectId);
    Task UpdateSubjectToRemoveFileAsync(int subjectId);
    Task UpdateAsync(SubjectFormDto dto, int subjectId);
    Task UpdateSubjectToAddFileAsync(IFormFile file, int subjectId);
    Task<List<SubjectDto>> GetNonDefaultSubjectAsync(YearType year, SemesterType semester);
    Task<int> AddLectureAsync(LectureFormDto dto);
    Task<string> GetLectureFileNameById(int lectureId);
    Task UpdateLectureAsync(LectureFormDto dto, int lectureId);
    Task UpdateLectureToAddFileAsync(IFormFile file, int lectureId);
    Task UpdateLectureToRemoveFileAsync(int lectureId);
    Task<List<LectureDto>> GetAllLectureAsync(YearType? year, SemesterType? semester, bool? isPractical, int? subjectId, string title);
    Task<LectureDto> GetLectureByIdAsync(int lectureId);
    Task RemoveLectureAsync(int lectureId);
}
