using FourthPro.Dto.Student;
using FourthPro.Repository.User;
using FourthPro.Service.Base;
using FourthPro.Shared.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FourthPro.Service.UserService;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepo userRepo;

    private string Key { get; set; }
    private string Issuer { get; set; }
    private string Audience { get; set; }
    public UserService(IUserRepo userRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        : base(configuration, httpContextAccessor)
    {
        this.userRepo = userRepo;
        Key = this.configuration["JwtConfig:secret"];
        Issuer = this.configuration["JwtConfig:validIssuer"];
        Audience = this.configuration["JwtConfig:validAudience"];
    }
    public async Task<string> SignUpAsync(UserFormDto dto)
    {
        var hashPassword = HashPassword(dto.Password);

        var id = await userRepo.SignUpAsync(dto, hashPassword);
        var token = await CreateTokenAsync(true, id, 1);
        return token;
    }
    public async Task<List<UserDto>> GetAllAsync()
    {
        //      if (CurrentUserId == -1)
        //          throw new Exception("You do not have Authorize..");

        //if (await userRepo.CheckIfStudentByIdentifier(CurrentUserId))
        //    throw new Exception("You do not have permission..");

        var result = await userRepo.GetAllUser();
        return result;
    }
    public async Task<UserDto> GetUserByIdentifierAsync(int identifier)
    {

        var result = await userRepo.GetUserByIdentifierAsync(identifier);
        return result;
    }
    public async Task<int> GetUsersCountAsync(YearType? year)
    {
        if (CurrentUserId == -1)
            throw new Exception("You do not have Authorize..");

        if (await userRepo.CheckIfStudentByIdentifier(CurrentUserId))
            throw new Exception("You do not have permission..");

        return await userRepo.GetUsersCountAsync(year);
    }

    public async Task<string> CreateTokenAsync(bool isStudent, long id, int role /*admin\student*/)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, id.ToString()),
            new(ClaimTypes.Role, role.ToString()),
            new("role", role.ToString())
        };
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: Issuer, audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMonths(10), // Ensure this line is correctly setting the expiration,
            signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        var bearerToken = "Bearer " + token;

        return bearerToken;
    }
    private string HashPassword(string password)
    {
        var passwordHasher = new PasswordHasher<object>();
        return passwordHasher.HashPassword(null, password);
    }
    public int GetCurrentUserId()
    {
        var claim = httpContextAccessor.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
        if (claim == null)
            return -1;
        return int.Parse(claim.Value);
    }
}