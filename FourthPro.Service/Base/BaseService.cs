using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FourthPro.Service.Base;

public class BaseService
{
    protected readonly IConfiguration configuration;
    protected readonly IHttpContextAccessor httpContextAccessor;

    public BaseService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this.httpContextAccessor = httpContextAccessor;
    }
    public int CurrentUserId
    {
        get
        {
            return GetCurrentUserId();
        }
    }
    public string CurrentJwtId
    {
        get
        {
            return GetCurrentJwtId();
        }
    }
    public string GetCurrentJwtId()
    {
        var claim = httpContextAccessor.HttpContext.User.FindFirst(c => c.Type == JwtRegisteredClaimNames.Jti);
        if (claim == null)
            return "";
        return claim.ToString().Split("jti: ").ElementAt(1);
    }
    public int GetCurrentUserId()
    {
        var claim = httpContextAccessor.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
        if (claim == null)
            return -1;
        return int.Parse(claim.Value);
    }
}
