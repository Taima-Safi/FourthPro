using FourthPro.Database.Context;
using FourthPro.Database.Model;
using FourthPro.Dto.Department;
using FourthPro.Dto.Doctor;
using FourthPro.Dto.Project;
using FourthPro.Dto.Student;
using FourthPro.Shared.Enum;
using Microsoft.EntityFrameworkCore;

namespace FourthPro.Repository.User;

public class UserRepo : IUserRepo
{
    private readonly FourthProDbContext context;
    public UserRepo(FourthProDbContext context)
    {
        this.context = context;
    }
    public async Task<int> SignUpAsync(UserFormDto dto, string hashPassword)
    {
        var user = await context.User.AddAsync(new UserModel
        {
            Name = dto.Name,
            Role = dto.Role,
            Year = dto.Year,
            Email = dto.Email,
            HashedPassword = hashPassword,
            Identifier = dto.Identifier
        });
        await context.SaveChangesAsync();
        return user.Entity.Id;
    }
    public async Task AddTokenAsync(string token, int userId)
    {
        await context.UserToken.AddAsync(new UserTokenModel
        {
            Token = token,
            UserId = userId,
            IsActive = true
        });
        await context.SaveChangesAsync();
    }
    public async Task<UserTokenModel> GetUserTokenActiveAsync(int userId) => await context.UserToken
        .Where(t => t.UserId == userId && t.IsActive == true).Include(u => u.User).FirstOrDefaultAsync();
    public async Task<bool> CheckIfTokenActiveAsync(string token) => await context.UserToken
        .Where(t => t.Token == token && t.IsActive == true).AnyAsync();
    public async Task RemoveUserTokenAsync(int userId)
    => await context.UserToken.Where(t => t.UserId == userId).ExecuteUpdateAsync(t => t.SetProperty(t => t.IsActive, false));
    public async Task RemoveTokenAsync(string token)
    {
        await context.UserToken.Where(t => t.Token == token).ExecuteUpdateAsync(t => t.SetProperty(t => t.IsActive, false));
    }

    public async Task<YearType> GetStudentYearAsync(int id)
        => await context.User.Where(u => u.Id == id).Select(u => u.Year).FirstOrDefaultAsync();

    public async Task<bool> CheckIfStudentByIdentifierAsync(int id)
        => await context.User.AnyAsync(u => u.Id == id && u.Identifier != 0);

    public async Task<bool> CheckIfExistAsync(int identifier)
        => await context.User.AnyAsync(u => u.Identifier == identifier);

    public bool checkIfAdmin(int id)
    {
        var x = context.User.Any(u => u.Id == id && u.Identifier == 0);
        return x;
    }
    public async Task<RoleType> GetRoleAsync(int id)
    {
        var x = await context.User.Where(u => u.Id == id).Select(u => u.Role).FirstOrDefaultAsync();
        return x;
    }
    public async Task<List<UserDto>> GetAllUser()
    {
        return await context.User.Select(u => new UserDto
        {
            Identifier = u.Identifier,
            Name = u.Name,
            Year = u.Year,
            Role = u.Role,
            Email = u.Email,
        }).ToListAsync();
    }
    public async Task<UserDto> GetUserByIdAsync(int id)
        => await context.User.Where(u => u.Id == id).Select(u => new UserDto
        {
            Identifier = u.Identifier,
            Name = u.Name,
            Year = u.Year,
            Role = u.Role
        }).FirstOrDefaultAsync();

    public async Task<UserModel> GetUserModelAsync(int identifier)
        => await context.User.Where(u => u.Identifier == identifier).FirstOrDefaultAsync();

    public async Task<UserDto> GetUserByIdentifierAsync(int identifier)
        => await context.User.Where(u => u.Identifier == identifier).Select(u => new UserDto
        {
            Identifier = u.Identifier,
            Name = u.Name,
            Year = u.Year,
            Role = u.Role,
            Email = u.Email,
            FifthProject = u.FifthProjectId != null ? new ProjectDto
            {
                Description = u.FifthProject.Description,
                Title = u.FifthProject.Title,
                Tools = u.FifthProject.Tools,
                Type = u.FifthProject.Type,
                Id = u.FifthProject.Id,
                Doctor = new DoctorDto
                {
                    Id = u.FifthProject.Doctor.Id,
                    Name = u.FifthProject.Doctor.Name,
                    Department = new DepartmentDto
                    {
                        Id = u.FifthProject.Doctor.Department.Id,
                        Title = u.FifthProject.Doctor.Department.Title
                    }
                }
            } : null,
            FourthProject = u.FourthProjectId != null ? new ProjectDto
            {
                Description = u.FourthProject.Description,
                Title = u.FourthProject.Title,
                Tools = u.FourthProject.Tools,
                Type = u.FourthProject.Type,
                Id = u.FourthProject.Id,
                Doctor = new DoctorDto
                {
                    Id = u.FourthProject.Doctor.Id,
                    Name = u.FourthProject.Doctor.Name,
                    Department = new DepartmentDto
                    {
                        Id = u.FourthProject.Doctor.Department.Id,
                        Title = u.FourthProject.Doctor.Department.Title
                    }
                }
            } : null,
        }).FirstOrDefaultAsync();

    public async Task<int> GetUsersCountAsync(YearType? year)
        => await context.User.Where(u => u.Role != 0 && (!year.HasValue || u.Year == year)).CountAsync();

    public async Task ChangePasswordAsync(string newHashPassword, long id)
    => await context.User.Where(u => u.Id == id)
    .ExecuteUpdateAsync(u => u.SetProperty(u => u.HashedPassword, newHashPassword));

}
