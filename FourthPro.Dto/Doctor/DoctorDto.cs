using FourthPro.Dto.Department;

namespace FourthPro.Dto.Doctor;

public class DoctorDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DepartmentDto Department { get; set; }
}
