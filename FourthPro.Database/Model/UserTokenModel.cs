namespace FourthPro.Database.Model;

public class UserTokenModel
{
    public int Id { get; set; }
    public string Token { get; set; }
    public bool IsActive { get; set; }

    public int UserId { get; set; }
    public UserModel User { get; set; }
}
