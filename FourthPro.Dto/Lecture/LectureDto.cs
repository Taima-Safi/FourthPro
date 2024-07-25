using FourthPro.Dto.Subject;

namespace FourthPro.Dto.Lecture;

public class LectureDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string File { get; set; }
    public bool IsPractical { get; set; }
    public SubjectDto Subject { get; set; }
}
