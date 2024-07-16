
using FourthPro.Dto.Department;

namespace FourthPro.Repository.Department;

public interface IDepartmentRepo
{
    Task<int> AddAsync(string name);
    Task<List<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto> GetByIdAsync(int departmentId);
    Task<int> GetDepartmentsCountAsync();
    Task RemoveAsync(int departmentId);
    Task UpdateAsync(int departmentId, string name);
}
