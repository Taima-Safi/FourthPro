using FourthPro.Shared.Enum;

namespace FourthPro.Database.Model;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RoleType Role { get; set; }
    public string Password { get; set; }
}
