using FourthPro.Shared.Enum;

namespace FourthPro.Database.Model;

public class SubjectModel
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
    public int DoctorId { get; set; }
    public DoctorModel Doctor { get; set; }
    public ICollection<StudentSubjectModel> StudentSubject { get; set; }

}
