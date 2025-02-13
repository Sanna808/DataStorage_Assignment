using Buisness.Factories;
using Buisness.Interfaces;
using Buisness.Models;
using Data.Enteties;
using Data.Interfaces;
using System.Linq.Expressions;

namespace Buisness.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<Project> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var entity = await _projectRepository.GetAsync(x => x.Titel == form.Titel);
        entity ??= await _projectRepository.CreateAsync(ProjectFactory.Create(form));

        return ProjectFactory.Create(entity);
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(ProjectFactory.Create);
        return projects ?? [];
    }

    public async Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        var project = ProjectFactory.Create(entity);
        return project ?? null!;
    }

    public async Task<Project> UpdateProjectAsync(ProjectUpdateForm form)
    {
        var existingEntity = await _projectRepository.GetAsync(x => x.Id == form.Id);
        if (existingEntity == null)
            return null!;

        existingEntity.Titel = form.Titel ?? existingEntity.Titel;
        existingEntity.Description = form.Description ?? existingEntity.Description;

        if (form.StartDate.HasValue)
            existingEntity.StartDate = form.StartDate.Value;

        if (form.EndDate.HasValue)
            existingEntity.EndDate = form.EndDate.Value;

        var result = await _projectRepository.UpdateAsync(x => x.Id == form.Id, existingEntity);
        if (result == null)
            return null!;

        return ProjectFactory.Create(result);
    }

    public async Task<bool> DeleteProjectAsync(int Id)
    {
        var result = await _projectRepository.DeleteAsync(x => x.Id == Id);
        return result;
    }

}
