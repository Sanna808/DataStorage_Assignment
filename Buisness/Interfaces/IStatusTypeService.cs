using Buisness.Models;

namespace Buisness.Interfaces
{
    public interface IStatusTypeService
    {
        Task<StatusType> CreateStatusTypeAsync(StatusTypeRegistrationForm form);
        Task<bool> DeleteStatusAsync(int id);
        Task<IEnumerable<StatusType>> GetAllStatusTypesAsync();
        Task<StatusType> GetStatusByNameAsync(string statusName);
        Task<StatusType> UppdateStatusTypeAsync(StatusTypeUpdateForm form);
    }
}