using FourthPro.Dto.Project;
using FourthPro.Dto.Student;

namespace FourthPro.Repository.Project;

public interface IProjectRepo
{
    Task<int> AddAsync(ProjectFormDto dto);
    Task<List<ProjectDto>> GetAllProjectByYearAsync(int? fourthProjectId, int? fifthProjectId);
    Task<ProjectDto> GetProjectByIdAsync(int projectId);
    Task<List<UserDto>> GetProjectUsersAsync(int projectId);
    Task RemoveAsync(int projectId);
    Task UpdateAsync(ProjectFormDto dto, int projectId);
}
