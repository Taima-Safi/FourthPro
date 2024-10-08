using FourthPro.Config;
using FourthPro.Database.Context;
using FourthPro.Middleware;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ErrorHandlerMiddleware>();
//builder.Services.AddTransient<AuthMiddleware>();
builder.Services.AddHttpContextAccessor();
#region Database
var connectionString = builder.Configuration.GetConnectionString(builder.Environment.IsProduction() ? "ServerTest" : "ServerTest");
builder.Services.AddDbContext<FourthProDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
#endregion

#region Caching Configuration
builder.Services.AddMemoryCache(opt =>
{
    opt.TrackLinkedCacheEntries = true;
}).AddResponseCaching();
#endregion

builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureRepos();
builder.Services.ConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials());

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSwagger();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.DocExpansion(DocExpansion.None);
    s.DisplayRequestDuration();
    s.EnableTryItOutByDefault();
});
app.UseRouting();

app.UseMiddleware<ErrorHandlerMiddleware>();
//app.UseMiddleware<AuthMiddleware>();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
