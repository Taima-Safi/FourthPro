using FourthPro.Database.Context;
using FourthPro.Database.Model;
using FourthPro.Dto.Student;
using Microsoft.EntityFrameworkCore;

namespace FourthPro.Repository.User;

public class UserRepo : IUserRepo
{
    private readonly FourthProDbContext context;
    public UserRepo(FourthProDbContext context)
    {
        this.context = context;
    }
    public async Task<int> AddAsync(StudentDto dto, string hashPassword)
    {
        var user = await context.User.AddAsync(new UserModel
        {
            Name = dto.Name,
            Role = dto.Role,
            Password = hashPassword,
        });
        await context.SaveChangesAsync();
        return user.Entity.Id;
    }
    public async Task<bool> CheckIfStudentById(int id)
        => await context.User.Where(u => u.Id == id && u.Role == Shared.Enum.RoleType.Student).AnyAsync();

    public async Task<List<StudentDto>> GetAllUser()
    {
        return await context.User.Select(u => new StudentDto
        {
            Id = u.Id,
            Name = u.Name
        }).ToListAsync();
    }
}
