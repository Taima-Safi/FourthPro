using FourthPro.Dto.Lecture;
using FourthPro.Dto.Subject;
using FourthPro.Repository.Doctor;
using FourthPro.Repository.Subject;
using FourthPro.Repository.User;
using FourthPro.Service.Base;
using FourthPro.Shared.Enum;
using FourthPro.Shared.Exception;
using FourthPro.Uploads;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FourthPro.Service.Subject;

public class SubjectService : BaseService, ISubjectService
{
    private readonly ISubjectRepo subjectRepo;
    private readonly IDoctorRepo doctorRepo;
    private readonly IUserRepo userRepo;

    public SubjectService(ISubjectRepo subjectRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IDoctorRepo doctorRepo, IUserRepo userRepo)
        : base(configuration, httpContextAccessor)
    {
        this.subjectRepo = subjectRepo;
        this.doctorRepo = doctorRepo;
        this.userRepo = userRepo;
    }
    public async Task<int> AddAsync(SubjectFormDto dto)
    {
        if (!await doctorRepo.CheckIfExistAsync(dto.DoctorId))
            throw new NotFoundException("Doctor not found..");

        string fileName = null;
        if (dto.LastQuestionsFile != null)
            fileName = FileHelper.UploadFile(dto.LastQuestionsFile, FileType.LastQuestionsFile);

        return await subjectRepo.AddAsync(dto, fileName);
    }
    public async Task UpdateAsync(SubjectFormDto dto, int subjectId)
    {
        if (!await subjectRepo.CheckIfExistAsync(subjectId))
            throw new NotFoundException("Subject not found..");

        if (!await doctorRepo.CheckIfExistAsync(dto.DoctorId))
            throw new NotFoundException("Doctor not found..");

        await subjectRepo.UpdateAsync(dto, subjectId);
    }
    public async Task<string> UpdateSubjectToAddFileAsync(IFormFile file, int subjectId)
    {
        try
        {

            if (!await subjectRepo.CheckIfExistAsync(subjectId))
                throw new NotFoundException("Subject not found..");
            if (file == null)
                throw new NotFoundException("You have to add file..");

            var fileName = FileHelper.UploadFile(file, FileType.LastQuestionsFile);

            await subjectRepo.UpdateSubjectToAddFileAsync(fileName, subjectId);
            return fileName;
        }
        catch
        {
            throw;
        }
    }
    public async Task UpdateSubjectToRemoveFileAsync(int subjectId)
    {
        if (!await subjectRepo.CheckIfExistAsync(subjectId))
            throw new NotFoundException("Subject not found..");

        await subjectRepo.UpdateSubjectToRemoveFileAsync(subjectId);
    }
    public async Task SelectFromOptionalSubjectsAsync(int subjectId)
    {

        await subjectRepo.SelectFromOptionalSubjectsAsync(subjectId, CurrentUserId);
    }
    public async Task<string> GetLastQuestionsFileNameById(int subjectId)
    {
        if (!await subjectRepo.CheckIfExistAsync(subjectId))
            throw new NotFoundException("Subject not found..");

        var fileName = await subjectRepo.GetLastQuestionsFileNameById(subjectId);
        if (string.IsNullOrEmpty(fileName))
            throw new NotFoundException("This subject does not have file");

        return fileName;
    }
    public async Task<List<SubjectDto>> GetAllAsync(YearType? year, SemesterType? semester, bool? isDefault, string title)
    {
        return await subjectRepo.GetAllAsync(year, semester, isDefault, title);
    }
    public async Task<List<SubjectDto>> GatAllCurrentUserSubjectAsync(YearType? year)
    {
        //var studentYear = await userRepo.GetStudentYearAsync(CurrentUserId);
        return await subjectRepo.GatAllCurrentUserSubjectAsync(CurrentUserId, year);
    }
    public async Task<SubjectDto> GetByIdAsync(int subjectId)
    {
        return await subjectRepo.GetByIdAsync(subjectId) ??
            throw new NotFoundException("Subject not found..");
    }
    public async Task<List<SubjectDto>> GetNonDefaultSubjectAsync(YearType year, SemesterType semester)
    {
        return await subjectRepo.GetNonDefaultSubjectAsync(year, semester);
    }
    public async Task<int> GetSubjectCountAsync(YearType? year, SemesterType? semester)
    {
        return await subjectRepo.GetSubjectCountAsync(year, semester);
    }
    public async Task RemoveAsync(int subjectId)
    {
        if (!await subjectRepo.CheckIfExistAsync(subjectId))
            throw new NotFoundException("Subject not found..");

        await subjectRepo.RemoveAsync(subjectId);
    }

    #region Lecture
    public async Task<int> AddLectureAsync(LectureFormDto dto)
    {
        if (!await subjectRepo.CheckIfExistAsync(dto.SubjectId))
            throw new NotFoundException("Subject not found..");

        string fileName = null;
        if (dto.LectureFile != null)
            fileName = FileHelper.UploadFile(dto.LectureFile, FileType.Lecture);

        return await subjectRepo.AddLectureAsync(dto, fileName);
    }
    public async Task<string> GetLectureFileNameById(int lectureId)
    {
        if (!await subjectRepo.CheckIfLectureExistAsync(lectureId))
            throw new NotFoundException("Lecture not found..");

        var fileName = await subjectRepo.GetLectureFileNameById(lectureId);
        if (string.IsNullOrEmpty(fileName))
            throw new NotFoundException("This lecture does not have file");

        return fileName;
    }
    public async Task UpdateLectureAsync(LectureFormDto dto, int lectureId)
    {
        if (!await subjectRepo.CheckIfLectureExistAsync(lectureId))
            throw new NotFoundException("Lecture not found..");

        if (!await subjectRepo.CheckIfExistAsync(dto.SubjectId))
            throw new NotFoundException("Subject not found..");

        await subjectRepo.UpdateLectureAsync(dto, lectureId);
    }
    public async Task UpdateLectureToAddFileAsync(IFormFile file, int lectureId)
    {
        if (!await subjectRepo.CheckIfLectureExistAsync(lectureId))
            throw new NotFoundException("Lecture not found..");
        if (file == null)
            throw new NotFoundException("You have to add file..");

        var fileName = FileHelper.UploadFile(file, FileType.Lecture);

        await subjectRepo.UpdateLectureToAddFileAsync(fileName, lectureId);
    }
    public async Task UpdateLectureToRemoveFileAsync(int lectureId)
    {
        if (!await subjectRepo.CheckIfLectureExistAsync(lectureId))
            throw new NotFoundException("Lecture not found..");

        await subjectRepo.UpdateLectureToRemoveFileAsync(lectureId);
    }
    public async Task<List<LectureDto>> GetAllLectureAsync(YearType? year, SemesterType? semester, bool? isPractical, int? subjectId, string title)
    {
        return await subjectRepo.GetAllLectureAsync(year, semester, isPractical, subjectId, title);
    }
    public async Task<LectureDto> GetLectureByIdAsync(int lectureId)
    {
        return await subjectRepo.GetLectureByIdAsync(lectureId) ??
            throw new NotFoundException("Lecture not found..");
    }
    public async Task RemoveLectureAsync(int lectureId)
    {
        if (!await subjectRepo.CheckIfLectureExistAsync(lectureId))
            throw new NotFoundException("Lecture not found..");

        await subjectRepo.RemoveLectureAsync(lectureId);
    }
    #endregion
}