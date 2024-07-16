
using FourthPro.Dto.Student;

namespace FourthPro.Repository.User;

public interface IUserRepo
{
    Task<int> AddAsync(StudentDto dto, string hashPassword);
    Task<bool> CheckIfStudentById(int id);
    Task<List<StudentDto>> GetAllUser();
}
