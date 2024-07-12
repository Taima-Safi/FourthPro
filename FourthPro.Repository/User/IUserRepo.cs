
using FourthPro.Dto.Student;

namespace FourthPro.Repository.User;

public interface IUserRepo
{
    Task<int> AddAsync(StudentDto dto);
    Task<List<StudentDto>> GetAllUser();
}
