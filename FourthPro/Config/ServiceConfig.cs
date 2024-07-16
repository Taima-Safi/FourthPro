using FourthPro.Repository.Doctor;
using FourthPro.Repository.User;
using FourthPro.Service.Doctor;
using FourthPro.Service.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FourthPro.Config;

public static class ServiceConfig
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        => services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration.GetSection("JwtConfig:validAudience").Value,
                ValidIssuer = configuration.GetSection("JwtConfig:validIssuer").Value,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:secret"])),
                ClockSkew = TimeSpan.Zero
            };
        });

    public static void ConfigureSwagger(this IServiceCollection services)
     => services.AddSwaggerGen(c =>
     {
         c.SwaggerDoc("v1", new OpenApiInfo
         {
             Title = Environment.GetEnvironmentVariable("ASPNETCORE_SWAGGER_TITLE"),
             Version = "v1",
             Description = "FourthPro API Services.",
             Contact = new OpenApiContact
             {
                 Name = "FourthPro"
             },
         });
         c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
         c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
             Name = "Authorization",
             Type = SecuritySchemeType.ApiKey,
             Scheme = "Bearer",
             BearerFormat = "JWT",
             In = ParameterLocation.Header,
             Description = "JWT Authorization header using the Bearer scheme."
         });
         c.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
         });
     });
    public static void ConfigureRepos(this IServiceCollection services)
   => services.AddScoped<IUserRepo, UserRepo>()
        .AddScoped<IDoctorRepo, DoctorRepo>();


    public static void ConfigureServices(this IServiceCollection services)
   => services.AddScoped<IUserService, UserService>()
   .AddScoped<IDoctorService, DoctorService>();
}
