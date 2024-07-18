using FourthPro.Shared.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FourthPro.Database.Model;

public class ProjectModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Tools { get; set; }
    public ProjectType Type { get; set; }
    public int DoctorId { get; set; }
    public DoctorModel Doctor { get; set; }
    [NotMapped]
    public ICollection<UserModel> Users { get; set; }
}
