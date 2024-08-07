using FourthPro.Shared.Enum;
using Microsoft.AspNetCore.Http;

namespace FourthPro.Dto.Project;

public class ProjectFormDto
{
    public string Url { get; set; }
    public string Title { get; set; }
    public string Tools { get; set; }
    public IFormFile? File { get; set; }
    public SectionType Type { get; set; }
    public string Description { get; set; }

    public int DoctorId { get; set; }
    public List<int> UserIds { get; set; }
}
