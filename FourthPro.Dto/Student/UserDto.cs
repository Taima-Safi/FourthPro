using FourthPro.Dto.Project;
using FourthPro.Shared.Enum;

namespace FourthPro.Dto.Student;

public class UserDto
{
    public int Identifier { get; set; }//University Number
    public string Password { get; set; }
    public YearType Year { get; set; }
    public RoleType Role { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public ProjectDto FourthProject { get; set; }
    public ProjectDto FifthProject { get; set; }
}
