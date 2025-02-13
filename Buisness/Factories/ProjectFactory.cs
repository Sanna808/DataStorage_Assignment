using Buisness.Models;
using Data.Enteties;

namespace Buisness.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create() => new();

    public static ProjectEntity Create(ProjectRegistrationForm form) => new()
    {
        Titel = form.Titel,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
    };

    public static Project Create(ProjectEntity entity) => new()
    {
        Id = entity.Id,
        Titel = entity.Titel,
        Description = entity.Description,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate,
    };

    public static ProjectUpdateForm Create(Project project) => new()
    {
        Id = project.Id,
        Titel = project.Titel,
        Description = project.Description,
        StartDate = project.StartDate,
        EndDate = project.EndDate,
    };

    public static ProjectEntity Create(ProjectUpdateForm form) => new()
    {
        Id = form.Id,
        Titel = form.Titel,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate,
    };
}
