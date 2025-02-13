using Buisness.Models;
using Data.Interfaces;

namespace Buisness.Services;

public class ProjectService(IProjectRepository projectRepository)
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<Project> CreateProjectAsync(ProjectRegistrationForm)
    {

    } 
}
