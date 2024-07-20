using FourthPro.Shared.Enum;
using Microsoft.AspNetCore.Http;

namespace FourthPro.Dto.Subject;

public class SubjectFormDto
{
    public string Title { get; set; }
    public YearType Year { get; set; }
    public bool IsDefault { get; set; }
    public SectionType Type { get; set; }
    public string Description { get; set; }
    public SemesterType Semester { get; set; }
    public IFormFile? LastQuestionsFile { get; set; }

    public int DoctorId { get; set; }
}
