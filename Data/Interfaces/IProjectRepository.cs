using Data.Enteties;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    new Task<IEnumerable<ProjectEntity>> GetAllAsync();

    new Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression);
}
