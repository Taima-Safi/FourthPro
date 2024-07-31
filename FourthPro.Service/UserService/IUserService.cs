using FourthPro.Dto.Student;
using FourthPro.Shared.Enum;

namespace FourthPro.Service.UserService;

public interface IUserService
{
    Task<string> SignUpAsync(UserFormDto dto);
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto> GetUserByIdentifierAsync(int identifier);
    Task<int> GetUsersCountAsync(YearType? year);
    Task<string> SignInAsync(int identifier, string password);
    Task SignOutAsync();
    Task<string> CreateTokenAsync(int userId, RoleType role);
}
