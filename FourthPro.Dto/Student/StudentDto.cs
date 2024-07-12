using FourthPro.Shared.Enum;

namespace FourthPro.Dto.Student;

public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public RoleType Role { get; set; }
    public string Password { get; set; }
}
