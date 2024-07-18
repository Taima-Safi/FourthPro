using FourthPro.Shared.Enum;

namespace FourthPro.Database.Model;

public class UserModel
{
    public int Id { get; set; }
    public int Identifier { get; set; }//University Number
    public string Password { get; set; }
    public YearType Year { get; set; }
    public RoleType Role { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public int? FourthProjectId { get; set; }
    public ProjectModel FourthProject { get; set; }

    public int? FifthProjectId { get; set; }
    public ProjectModel FifthProject { get; set; }
    public ICollection<StudentSubjectModel> StudentSubject { get; set; }

}
