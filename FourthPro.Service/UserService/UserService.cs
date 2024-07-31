using FourthPro.Dto.Student;
using FourthPro.Repository.Subject;
using FourthPro.Repository.User;
using FourthPro.Service.Base;
using FourthPro.Shared.Enum;
using FourthPro.Shared.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FourthPro.Service.UserService;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepo userRepo;
    private readonly ISubjectRepo subjectRepo;

    private string Key { get; set; }
    private string Issuer { get; set; }
    private string Audience { get; set; }
    public UserService(IUserRepo userRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ISubjectRepo subjectRepo)
        : base(configuration, httpContextAccessor)
    {
        this.userRepo = userRepo;
        Key = this.configuration["JwtConfig:secret"];
        Issuer = this.configuration["JwtConfig:validIssuer"];
        Audience = this.configuration["JwtConfig:validAudience"];
        this.subjectRepo = subjectRepo;
    }
    public async Task<string> SignUpAsync(UserFormDto dto)
    {
        var hashPassword = HashPassword(dto.Password);

        var userId = await userRepo.SignUpAsync(dto, hashPassword);
        //TODO : Add default subjects to user (i have to know the semester)
        //var subjects = await subjectRepo.GetAllAsync(dto.Year, );
        //var subjects = new StudentSubjectModel();
        //switch (dto.Year)
        //{
        //    case YearType.First:
        //        subjects = new StudentSubjectModel
        //        {
        //            UserId = id,
        //            SubjectId = ,
        //        }
        //        break;
        //    case YearType.Second:
        //        break;
        //    case YearType.Third:
        //        break;
        //    case YearType.Fourth:
        //        break;
        //    case YearType.Fifth:
        //        break;
        //}
        var token = await CreateTokenAsync(userId, RoleType.Student);
        return token;
    }
    public async Task<string> SignInAsync(int identifier, string password)
    {
        var x = CurrentJwtId;
        var user = await userRepo.GetUserModelAsync(identifier) ??
            throw new NotFoundException("Password Or Identifier Wrong");

        await CheckPasswordCorrectness(password, user.HashedPassword, user.Id);

        await userRepo.RemoveUserTokenAsync(user.Id);
        var token = await CreateTokenAsync(user.Id, user.Role);

        return token;
    }
    public async Task SignOutAsync()
    {
        if (string.IsNullOrEmpty(CurrentJwtId))
            throw new ValidationException("You do not have active session ");

        await userRepo.RemoveUserTokenAsync(CurrentUserId);
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

        if (await userRepo.CheckIfStudentByIdentifierAsync(CurrentUserId))
            throw new Exception("You do not have permission..");

        return await userRepo.GetUsersCountAsync(year);
    }

    public async Task<string> CreateTokenAsync(int userId, RoleType role /*admin\student*/)
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

        await userRepo.AddTokenAsync(bearerToken, userId);

        return bearerToken;
    }
    private string HashPassword(string password)
    {
        var passwordHasher = new PasswordHasher<object>();
        return passwordHasher.HashPassword(null, password);
    }

    private PasswordVerificationResult CheckPassword(string password, string hash, out string newHash)
    {
        var passwordHasher = new PasswordHasher<object>();
        var result = passwordHasher.VerifyHashedPassword(null, hash, password);
        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            newHash = HashPassword(password);
            return result;
        }
        else if (result == PasswordVerificationResult.Success)
        {
            newHash = hash;
            return result;
        }
        newHash = null;
        return result;
    }
    public async Task CheckPasswordCorrectness(string password, string hashPassword, long userId)
    {
        if (hashPassword == null)
            throw new ValidationException("Password Or Identifier Wrong");

        var isMatchPassword = CheckPassword(password, hashPassword, out string newHash);

        if (isMatchPassword == PasswordVerificationResult.Failed)
            throw new ValidationException("Password Or Identifier Wrong");

        if (isMatchPassword == PasswordVerificationResult.SuccessRehashNeeded)
            await userRepo.ChangePasswordAsync(newHash, userId);
    }
}