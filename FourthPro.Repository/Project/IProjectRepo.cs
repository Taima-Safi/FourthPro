using FourthPro.Dto.Project;
using FourthPro.Shared.Enum;

namespace FourthPro.Repository.Project;

public interface IProjectRepo
{
    Task<int> AddAsync(ProjectFormDto dto);
    Task<bool> CheckIfExistAsync(int projectId);
    Task<List<ProjectDto>> GetAllAsync(int? fourthProjectId, int? fifthProjectId);
    Task<ProjectDto> GetProjectByIdAsync(int projectId);
    Task<int> GetProjectCountAsync(int? doctorId, SectionType? type, string tool);
    //Task<List<UserDto>> GetProjectUsersAsync(int projectId);
    Task RemoveAsync(int projectId);
    Task UpdateAsync(ProjectFormDto dto, int projectId);
}
