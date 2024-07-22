﻿using FourthPro.Dto.Subject;
using FourthPro.Repository.Doctor;
using FourthPro.Repository.Subject;
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

    public SubjectService(ISubjectRepo subjectRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IDoctorRepo doctorRepo)
        : base(configuration, httpContextAccessor)
    {
        this.subjectRepo = subjectRepo;
        this.doctorRepo = doctorRepo;
    }
    public async Task<int> AddAsync(SubjectFormDto dto)
    {
        if (!await doctorRepo.CheckIfExist(dto.DoctorId))
            throw new NotFoundException("Doctor not found..");

        string fileName = null;
        if (dto.LastQuestionsFile != null)
            fileName = FileHelper.UploadFile(dto.LastQuestionsFile, false);

        return await subjectRepo.AddAsync(dto, fileName);
    }
    public async Task UpdateAsync(SubjectFormDto dto, int subjectId)
    {
        if (!await subjectRepo.CheckIfExistAsync(subjectId))
            throw new NotFoundException("Subject not found..");

        await subjectRepo.UpdateAsync(dto, subjectId);
    }
    public async Task UpdateSubjectToAddFileAsync(IFormFile file, int subjectId)
    {
        if (!await subjectRepo.CheckIfExistAsync(subjectId))
            throw new NotFoundException("Subject not found..");
        if (file == null)
            throw new NotFoundException("You have to add file..");

        var fileName = FileHelper.UploadFile(file, false);

        await subjectRepo.UpdateSubjectToAddFileAsync(fileName, subjectId);
    }
    public async Task UpdateSubjectToRemoveFileAsync(int subjectId)
    {
        if (!await subjectRepo.CheckIfExistAsync(subjectId))
            throw new NotFoundException("Subject not found..");

        await subjectRepo.UpdateSubjectToRemoveFileAsync(subjectId);
    }
    public async Task<string> GetLastQuestionsFileNameById(int subjectId)
    {
        if (!await subjectRepo.CheckIfExistAsync(subjectId))
            throw new NotFoundException("Subject not found..");

        var fileName = await subjectRepo.GetLastQuestionsFileNameById(subjectId);
        if (string.IsNullOrEmpty(fileName))
            throw new NotFoundException("This subject do not have file");

        return fileName;
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
}