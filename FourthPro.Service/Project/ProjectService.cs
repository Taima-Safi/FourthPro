using FourthPro.Dto.Project;
using FourthPro.Repository.Project;
using FourthPro.Service.Base;
using FourthPro.Shared.Enum;
using FourthPro.Shared.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FourthPro.Service.Project;

public class ProjectService : BaseService, IProjectService
{
    private readonly IProjectRepo projectRepo;

    public ProjectService(IProjectRepo projectRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        : base(configuration, httpContextAccessor)
    {
        this.projectRepo = projectRepo;
    }
    public async Task<int> AddAsync(ProjectFormDto dto)
    {
        return await projectRepo.AddAsync(dto);
    }

    public async Task<List<ProjectDto>> GetAllAsync(int? fourthProject, int? fifthProject)
    {
        return await projectRepo.GetAllAsync(fourthProject, fifthProject);
    }
    public async Task<ProjectDto> GetByIdAsync(int projectId)
    {
        return await projectRepo.GetProjectByIdAsync(projectId) ??
            throw new NotFoundException("Project not found..");
    }
    public async Task<int> GetProjectCountAsync(int? doctorId, SectionType? type, string? tool)
    {
        return await projectRepo.GetProjectCountAsync(doctorId, type, tool);
    }
    public async Task UpdateAsync(ProjectFormDto dto, int projectId)
    {
        if (!await projectRepo.CheckIfExistAsync(projectId))
            throw new NotFoundException("Project not found..");

        await projectRepo.UpdateAsync(dto, projectId);
    }
    public async Task RemoveAsync(int projectId)
    {
        if (!await projectRepo.CheckIfExistAsync(projectId))
            throw new NotFoundException("Project not found..");

        await projectRepo.RemoveAsync(projectId);
    }

}
