using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
    public int GetCurrentUserId()
    {
        var claim = httpContextAccessor.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
        if (claim == null)
            return -1;
        return int.Parse(claim.Value);
    }
}
