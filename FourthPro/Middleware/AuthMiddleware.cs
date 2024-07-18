using FourthPro.Repository.User;
using FourthPro.Shared.Exception;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FourthPro.Middleware;

public class AuthMiddleware : IMiddleware
{
    private readonly IConfiguration configuration;
    public AuthMiddleware(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            ////Get the target api.
            if (context.GetEndpoint() == null) { await next(context); return; }
            var endpoint = context.GetEndpoint();
            //Check if the api is AllowAnonymous.
            bool? isAllowAnonymous = endpoint?.Metadata.Any(x => x.GetType() == typeof(AllowAnonymousAttribute));
            if (isAllowAnonymous == true /*|| endpoint.DisplayName.Contains("/chatHub")*/)
            {
                await next(context);
                return;
            }

            //Dynamic Authorization here:
            var token = context.Request.Headers["Authorization"].ToString();
            string userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            //Check if he don't have a token or it's expired.
            if (string.IsNullOrEmpty(token) /*|| !context.User.Identity.IsAuthenticated*/)
                throw new UnauthorizedAccessException();

            var userRepo = context.RequestServices.GetRequiredService<IUserRepo>();

            //string jwtId = context.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
            //if (!await tokenRepo.CheckIfCurrentTokenValidAsync(jwtId))
            //    throw new UnauthorizedAccessException();


            //Get Controller Name.
            string controllerName = context.Request.RouteValues["controller"].ToString() ?? throw new NotFoundException("endpoint not found");

            //Get Api Name
            string actionType = context.Request.RouteValues["action"].ToString() ?? throw new NotFoundException("endpoint not found");

            if (controllerName.Contains("Dashboard"))
            {
                if (int.Parse(userId) == 0)
                {
                    await next(context);
                    return;
                }
            }
            throw new AccessViolationException("No Access");
        }
        catch (Exception)
        {
            throw;
        }
    }
}