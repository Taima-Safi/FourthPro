using FourthPro.Repository.Project;

namespace FourthPro.Service.Project;

public class ProjectService : IProjectService
{
    private readonly IProjectRepo projectRepo;

    public ProjectService(IProjectRepo projectRepo)
    {
        this.projectRepo = projectRepo;
    }

}
