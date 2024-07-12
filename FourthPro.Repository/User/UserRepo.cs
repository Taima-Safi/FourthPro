using FourthPro.Database.Context;
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
    public async Task<List<StudentDto>> GetAllUser()
    {
        return await context.User.Select(u => new StudentDto
        {
            Id = u.Id,
            Name = u.Name
        }).ToListAsync();
    }
}
