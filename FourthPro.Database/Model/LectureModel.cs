namespace FourthPro.Database.Model;

public class LectureModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string File { get; set; }
    public bool IsPractical { get; set; }

    public int SubjectId { get; set; }
    public SubjectModel Subject { get; set; }
}
