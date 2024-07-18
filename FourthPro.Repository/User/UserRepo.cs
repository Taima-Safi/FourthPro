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
            Password = hashPassword,
            Identifier = dto.Identifier
        });
        await context.SaveChangesAsync();
        return user.Entity.Id;
    }
    public async Task<bool> CheckIfStudentByIdentifier(int identifier)
        => await context.User.Where(u => u.Identifier == identifier && u.Role == Shared.Enum.RoleType.Student).AnyAsync();

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
    public async Task<UserDto> GetUserByIdentifierAsync(int identifier)
        => await context.User.Select(u => new UserDto
        {
            Identifier = u.Identifier,
            Name = u.Name,
            Year = u.Year,
            Role = u.Role,
            Email = u.Email,
            FifthProject = new ProjectDto
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
            },
            FourthProject = new ProjectDto
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
            },
        }).FirstOrDefaultAsync();

    public async Task<int> GetUsersCountAsync(YearType? year)
        => await context.User.Where(u => (!year.HasValue || u.Year == year)).CountAsync();
}
