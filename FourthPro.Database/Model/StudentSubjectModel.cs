namespace FourthPro.Database.Model;

public class StudentSubjectModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public int UserId { get; set; }
    public UserModel User { get; set; }
    public int SubjectId { get; set; }
    public SubjectModel Subject { get; set; }
}
