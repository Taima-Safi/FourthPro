using FourthPro.Dto.Project;
using FourthPro.Repository.Doctor;
using FourthPro.Repository.Project;
using FourthPro.Service.Base;
using FourthPro.Shared.Enum;
using FourthPro.Shared.Exception;
using FourthPro.Uploads;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FourthPro.Service.Project;

public class ProjectService : BaseService, IProjectService
{
    private readonly IProjectRepo projectRepo;
    private readonly IDoctorRepo doctorRepo;

    public ProjectService(IProjectRepo projectRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IDoctorRepo doctorRepo)
        : base(configuration, httpContextAccessor)
    {
        this.projectRepo = projectRepo;
        this.doctorRepo = doctorRepo;
    }
    public async Task<int> AddAsync(ProjectFormDto dto)
    {
        if (!await doctorRepo.CheckIfExistAsync(dto.DoctorId))
            throw new NotFoundException("Doctor not found..");

        if (await doctorRepo.GetDoctorProjectCountAsync(dto.DoctorId) >= 6)
            throw new NotFoundException("This doctor has the maximum count of Projects for this year");

        string fileName = null;
        if (dto.File != null)
            fileName = FileHelper.UploadFile(dto.File, FileType.Project);

        var projectId = await projectRepo.AddAsync(dto, fileName);

        foreach (var userId in dto.UserIds)
            await projectRepo.UpdateUserProjectAsync(projectId, dto.Semester, userId);

        return projectId;
    }

    public async Task<List<ProjectDto>> GetAllAsync(SectionType? type, DateTime? date)
    {
        return await projectRepo.GetAllAsync(type, date);
    }
    public async Task<List<ProjectDto>> GetUserProjectsAsync()
    {
        return await projectRepo.GetUserProjectsAsync(CurrentUserId);
    }
    public async Task<string> GetProjectFileNameByIdAsync(int projectId)
    {
        if (!await projectRepo.CheckIfExistAsync(projectId))
            throw new NotFoundException("Project not found..");

        var fileName = await projectRepo.GetProjectFileNameByIdAsync(projectId);
        if (string.IsNullOrEmpty(fileName))
            throw new NotFoundException("This project does not have file");

        return fileName;
    }
    public async Task<ProjectDto> GetByIdAsync(int projectId)
    {
        if (!await projectRepo.CheckIfExistAsync(projectId))
            throw new NotFoundException("Project not found..");

        return await projectRepo.GetProjectByIdAsync(projectId);
    }
    public async Task<int> GetProjectCountAsync(int? doctorId, SectionType? type, string tool)
    {
        //TODO : check if admin
        return await projectRepo.GetProjectCountAsync(doctorId, type, tool);
    }
    public async Task UpdateAsync(ProjectFormDto dto, int projectId)
    {
        if (!await projectRepo.CheckIfExistAsync(projectId))
            throw new NotFoundException("Project not found..");
        if (dto.DoctorId != 0)
        {
            if (!await doctorRepo.CheckIfExistAsync(dto.DoctorId))
                throw new NotFoundException("Doctor not found..");

            if (await doctorRepo.GetDoctorProjectCountAsync(dto.DoctorId) >= 6)
                throw new NotFoundException("This doctor has the maximum count of Projects for this year");
        }
        await projectRepo.UpdateAsync(dto, projectId);
    }
    public async Task RemoveAsync(int projectId)
    {
        //TODO : check if admin
        if (!await projectRepo.CheckIfExistAsync(projectId))
            throw new NotFoundException("Project not found..");

        await projectRepo.RemoveAsync(projectId);
    }

}
