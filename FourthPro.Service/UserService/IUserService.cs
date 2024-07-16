using FourthPro.Dto.Common;
using FourthPro.Dto.Student;

namespace FourthPro.Service.UserService;

public interface IUserService
{
    Task<Tuple<int, string>> AddAsync(StudentDto dto);
    Task<string> CreateTokenAsync(bool isStudent, long userId, int role);
    Task<CommonResponseDto<List<StudentDto>>> GetAllAsync();
    int GetCurrentUserId();
}
