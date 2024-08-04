using FourthPro.Dto.Project;
using FourthPro.Shared.Enum;

namespace FourthPro.Service.Project;

public interface IProjectService
{
    Task<int> AddAsync(ProjectFormDto dto, SemesterType semester);
    Task<List<ProjectDto>> GetAllAsync(int? fourthProject, int? fifthProject);
    Task<ProjectDto> GetByIdAsync(int projectId);
    Task<int> GetProjectCountAsync(int? doctorId, SectionType? type, string tool);
    Task<List<ProjectDto>> GetUserProjectsAsync();
    Task RemoveAsync(int projectId);
    Task UpdateAsync(ProjectFormDto dto, int projectId);
}
