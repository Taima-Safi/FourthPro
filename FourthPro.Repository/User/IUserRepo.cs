
using FourthPro.Dto.Student;
using FourthPro.Shared.Enum;

namespace FourthPro.Repository.User;

public interface IUserRepo
{
    Task<Tuple<int, int>> SignUpAsync(UserFormDto dto, string hashPassword);
    Task<bool> CheckIfStudentByIdentifier(int identifier);
    Task<List<UserDto>> GetAllUser();
    Task<UserDto> GetUserByIdentifierAsync(int identifier);
    Task<int> GetUsersCountAsync(YearType? year);
}
