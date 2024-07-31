using FourthPro.Database.Model;
using FourthPro.Dto.Student;
using FourthPro.Shared.Enum;

namespace FourthPro.Repository.User;

public interface IUserRepo
{
    Task<int> SignUpAsync(UserFormDto dto, string hashPassword);
    Task<bool> CheckIfStudentByIdentifierAsync(int id);
    Task<List<UserDto>> GetAllUser();
    Task<UserDto> GetUserByIdentifierAsync(int identifier);
    Task<int> GetUsersCountAsync(YearType? year);
    Task<UserDto> GetUserByIdAsync(int id);
    bool checkIfAdmin(int id);
    Task<RoleType> GetRoleAsync(int id);
    Task<bool> CheckIfExistAsync(int identifier);
    Task<UserModel> GetUserModelAsync(int identifier);
    Task ChangePasswordAsync(string newHashPassword, long id);
    Task AddTokenAsync(string token, int userId);
    Task RemoveUserTokenAsync(int userId);
    Task RemoveTokenAsync(string token);
    Task<UserTokenModel> GetUserTokenActiveAsync(int userId);
}
