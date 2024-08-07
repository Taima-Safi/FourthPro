using FourthPro.Dto.Doctor;
using FourthPro.Dto.Student;
using FourthPro.Shared.Enum;

namespace FourthPro.Dto.Project;

public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string File { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Tools { get; set; }
    public SectionType Type { get; set; }
    public DoctorDto Doctor { get; set; }
    public List<UserDto> Users { get; set; } = new();
}
