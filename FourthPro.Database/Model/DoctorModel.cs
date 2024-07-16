namespace FourthPro.Database.Model;

public class DoctorModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int DepartmentId { get; set; }
    public DepartmentModel Department { get; set; }
}
