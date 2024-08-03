using FourthPro.Dto.Doctor;
using FourthPro.Dto.Student;

namespace FourthPro.Dto.Project;

public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Tools { get; set; }
    public Shared.Enum.SectionType Type { get; set; }
    public DoctorDto Doctor { get; set; }
    public List<UserDto> Users { get; set; } = new();
}
