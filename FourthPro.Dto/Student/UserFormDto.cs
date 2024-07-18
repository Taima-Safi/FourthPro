using FourthPro.Shared.Enum;

namespace FourthPro.Dto.Student;

public class UserFormDto
{
    public int Identifier { get; set; }//University Number
    public string Password { get; set; }
    public YearType Year { get; set; }
    public RoleType Role { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
