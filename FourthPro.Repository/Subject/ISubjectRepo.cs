using FourthPro.Dto.Lecture;
using FourthPro.Dto.Subject;
using FourthPro.Shared.Enum;

namespace FourthPro.Repository.Subject;

public interface ISubjectRepo
{
    Task<int> AddAsync(SubjectFormDto dto, string? fileName);
    Task<bool> CheckIfExistAsync(int subjectId);
    Task<List<SubjectDto>> GetAllAsync(YearType? year, SemesterType? semester, bool? isDefault, string title);
    Task<SubjectDto> GetByIdAsync(int subjectId);
    Task<string> GetLastQuestionsFileNameById(int subjectId);
    Task<int> GetSubjectCountAsync(YearType? year, SemesterType? semester);
    Task RemoveAsync(int subjectId);
    Task UpdateSubjectToRemoveFileAsync(int subjectId);
    Task UpdateAsync(SubjectFormDto dto, int subjectId);
    Task UpdateSubjectToAddFileAsync(string fileName, int subjectId);
    Task<List<SubjectDto>> GetNonDefaultSubjectAsync(YearType year, SemesterType semester);
    Task<int> AddLectureAsync(LectureFormDto dto, string fileName);
    Task UpdateLectureAsync(LectureFormDto dto, int lectureId);
    Task UpdateLectureToAddFileAsync(string fileName, int lectureId);
    Task UpdateLectureToRemoveFileAsync(int lectureId);
    Task RemoveLectureAsync(int lectureId);
    Task<string> GetLectureFileNameById(int lectureId);
    Task<List<LectureDto>> GetAllLectureAsync(YearType? year, SemesterType? semester, bool? isPractice, int? subjectId, string title);
    Task<LectureDto> GetLectureByIdAsync(int lectureId);
    Task<bool> CheckIfLectureExistAsync(int lectureId);
    Task SelectFromOptionalSubjectsAsync(int subjectId, int userId);
}
