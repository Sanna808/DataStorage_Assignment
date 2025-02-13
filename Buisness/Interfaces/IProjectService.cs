using Buisness.Models;
using Data.Enteties;
using System.Linq.Expressions;

namespace Buisness.Interfaces;

public interface IProjectService
{
    Task<Project> CreateProjectAsync(ProjectRegistrationForm form);

    Task<bool> DeleteProjectAsync(int Id);

    Task<IEnumerable<Project>> GetAllProjectsAsync();

    Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression);

    Task<Project> UpdateProjectAsync(ProjectUpdateForm form);
}