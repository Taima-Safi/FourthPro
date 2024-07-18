using FourthPro.Shared.Enum;

namespace FourthPro.Dto.Project;

public class ProjectFormDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Tools { get; set; }
    public ProjectType Type { get; set; }
    public int DoctorId { get; set; }
}
