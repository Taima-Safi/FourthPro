namespace FourthPro.Database.Model;

public class DepartmentModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<DoctorModel> Doctors { get; set; }
}
