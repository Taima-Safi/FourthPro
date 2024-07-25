using Microsoft.AspNetCore.Http;

namespace FourthPro.Dto.Lecture;

public class LectureFormDto
{
    public string Title { get; set; }
    public IFormFile LectureFile { get; set; }
    public bool IsPractical { get; set; }

    public int SubjectId { get; set; }
}
