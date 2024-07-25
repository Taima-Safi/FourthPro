using FourthPro.Dto.Doctor;
using FourthPro.Dto.Lecture;
using FourthPro.Shared.Enum;

namespace FourthPro.Dto.Subject;

public class SubjectDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public YearType Year { get; set; }
    public bool IsDefault { get; set; }
    public SectionType Type { get; set; }
    public string Description { get; set; }
    public SemesterType Semester { get; set; }
    public string? File { get; set; }
    // [NotMapped]
    // public IFormFile LastQuestionsFile { get; set; }
    public DoctorDto Doctor { get; set; }
    //  public List<StudentSubjectModel> StudentSubject { get; set; } = new();
    public List<LectureDto> Lectures { get; set; } = new();

}
