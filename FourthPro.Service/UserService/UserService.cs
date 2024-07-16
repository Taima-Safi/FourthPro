using FourthPro.Dto.Common;
using FourthPro.Dto.Student;
using FourthPro.Repository.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FourthPro.Service.UserService;

public class UserService : IUserService
{
    private readonly IUserRepo userRepo;
    private readonly IConfiguration configuration;
    private readonly IHttpContextAccessor httpContextAccessor;

    private string Key { get; set; }
    private string Issuer { get; set; }
    private string Audience { get; set; }
    public UserService(IUserRepo userRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        this.userRepo = userRepo;
        this.configuration = configuration;
        Key = this.configuration["JwtConfig:secret"];
        Issuer = this.configuration["JwtConfig:validIssuer"];
        Audience = this.configuration["JwtConfig:validAudience"];
        this.httpContextAccessor = httpContextAccessor;
    }
    public async Task<Tuple<int, string>> AddAsync(StudentDto dto)
    {
        var hashPassword = HashPassword(dto.Password);

        var userId = await userRepo.AddAsync(dto, hashPassword);
        var token = await CreateTokenAsync(true, userId, 1);
        return new(userId, token);
    }
    public async Task<CommonResponseDto<List<StudentDto>>> GetAllAsync()
    {
        var userId = GetCurrentUserId();

        if (!await userRepo.CheckIfStudentById(userId))
            return new(new List<StudentDto>(), "test message");

        var result = await userRepo.GetAllUser();

        return new(result, "");
    }
    public async Task<string> CreateTokenAsync(bool isStudent, long userId, int role /*admin\student*/)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
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
