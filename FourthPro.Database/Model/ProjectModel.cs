
using FourthPro.Shared.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FourthPro.Database.Model;

public class ProjectModel
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string File { get; set; }
    public string Title { get; set; }
    public string Tools { get; set; }
    public DateTime Date { get; set; }
    public SectionType Type { get; set; }
    public string Description { get; set; }
    public SemesterType Semester { get; set; }

    public int DoctorId { get; set; }
    public DoctorModel Doctor { get; set; }
    [NotMapped]
    public ICollection<UserModel> Users { get; set; }
}
