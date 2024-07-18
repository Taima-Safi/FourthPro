using FourthPro.Repository.Project;
using FourthPro.Service.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FourthPro.Service.Project;

public class ProjectService : BaseService, IProjectService
{
    private readonly IProjectRepo projectRepo;

    public ProjectService(IProjectRepo projectRepo, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        : base(configuration, httpContextAccessor)
    {
        this.projectRepo = projectRepo;
    }

}
