using FourthPro.Dto.Project;
using FourthPro.Shared.Enum;

namespace FourthPro.Repository.Project;

public interface IProjectRepo
{
    Task<int> AddAsync(ProjectFormDto dto, string fileName);
    Task<bool> CheckIfExistAsync(int projectId);
    Task<List<ProjectDto>> GetAllAsync(SectionType? type, DateTime? date);
    Task<ProjectDto> GetProjectByIdAsync(int projectId);
    Task<int> GetProjectCountAsync(int? doctorId, SectionType? type, string tool);
    Task<string> GetProjectFileNameByIdAsync(int projectId);
    Task<List<ProjectDto>> GetUserProjectsAsync(int userId);

    //Task<List<UserDto>> GetProjectUsersAsync(int projectId);
    Task RemoveAsync(int projectId);
    Task UpdateAsync(ProjectFormDto dto, int projectId);
    Task UpdateUserProjectAsync(int projectId, SemesterType semester, int userId);
}
