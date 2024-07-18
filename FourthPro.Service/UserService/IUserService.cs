using FourthPro.Dto.Student;
using FourthPro.Shared.Enum;

namespace FourthPro.Service.UserService;

public interface IUserService
{
    Task<Tuple<int, string>> SignUpAsync(UserFormDto dto);
    Task<string> CreateTokenAsync(bool isStudent, long userId, int role);
    Task<List<UserDto>> GetAllAsync();
    int GetCurrentUserId();
    Task<UserDto> GetUserByIdentifierAsync(int identifier);
    Task<int> GetUsersCountAsync(YearType? year);
}
