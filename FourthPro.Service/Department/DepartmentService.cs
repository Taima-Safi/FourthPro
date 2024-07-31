using FourthPro.Dto.Department;
using FourthPro.Repository.Department;
using FourthPro.Repository.User;
using FourthPro.Service.Base;
using FourthPro.Shared.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FourthPro.Service.Department;

public class DepartmentService : BaseService, IDepartmentService
{
    private readonly IDepartmentRepo departmentRepo;
    private readonly IUserRepo userRepo;
    public DepartmentService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IDepartmentRepo departmentRepo, IUserRepo userRepo)
        : base(configuration, httpContextAccessor)
    {
        this.departmentRepo = departmentRepo;
        this.userRepo = userRepo;
    }
    public async Task<int> AddAsync(string name)
    {
        var x = await userRepo.GetUserTokenActiveAsync(CurrentUserId);
        if (x == null)
            throw new UnauthorizedAccessException("You do not have Authorize..");

        if (x.User.Role != Shared.Enum.RoleType.Admin)
            throw new UnauthorizedAccessException("You do not have permission to add department..");

        if (CurrentUserId == -1)
            throw new AccessViolationException("You do not have Authorize..");

        if (!userRepo.checkIfAdmin(CurrentUserId))
            throw new UnauthorizedAccessException("You do not have permission to add department..");

        var departmentId = await departmentRepo.AddAsync(name);
        return departmentId;
    }
    public async Task<List<DepartmentDto>> GetAllAsync()
    => await departmentRepo.GetAllAsync();

    public async Task<int> GetDepartmentsCountAsync()
        => await departmentRepo.GetDepartmentsCountAsync();

    public async Task<DepartmentDto> GetByIdAsync(int departmentId)
    {
        if (!await departmentRepo.CheckIfExist(departmentId))
            throw new NotFoundException("Department Not found..");

        return await departmentRepo.GetByIdAsync(departmentId);
    }

    public async Task UpdateAsync(int departmentId, string name)
    {
        if (CurrentUserId == -1)
            throw new AccessViolationException("You do not have Authorize..");

        if (await userRepo.CheckIfStudentByIdentifierAsync(CurrentUserId))
            throw new UnauthorizedAccessException("You do not have permission to edit department..");

        if (!await departmentRepo.CheckIfExist(departmentId))
            throw new NotFoundException("Department not found..");

        await departmentRepo.UpdateAsync(departmentId, name);
    }
    public async Task RemoveAsync(int departmentId)
    {
        if (CurrentUserId == -1)
            throw new AccessViolationException("You do not have Authorize..");

        if (await userRepo.CheckIfStudentByIdentifierAsync(CurrentUserId))
            throw new UnauthorizedAccessException("You do not have permission to delete department..");

        if (!await departmentRepo.CheckIfExist(departmentId))
            throw new NotFoundException("Department not found..");

        await departmentRepo.RemoveAsync(departmentId);
    }
}
