using Buisness.Factories;
using Buisness.Interfaces;
using Buisness.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Buisness.Services;

public class StatusTypeService(IStatusTypeRepository statusTypeRepository) : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository = statusTypeRepository;

    public async Task<StatusType> CreateStatusTypeAsync(StatusTypeRegistrationForm form)
    {
        var entity = await _statusTypeRepository.GetAsync(x => x.StatusName == form.StatusName);
        entity ??= await _statusTypeRepository.CreateAsync(StatusTypeFactory.Create(form));

        return StatusTypeFactory.Create(entity);
    }

    public async Task<IEnumerable<StatusType>> GetAllStatusTypesAsync()
    {
        var enteties = await _statusTypeRepository.GetAllAsync();
        var statusTypes = enteties.Select(StatusTypeFactory.Create);
        return statusTypes ?? [];
    }

    public async Task<StatusType> GetStatusByNameAsync(string statusName)
    {
        var statusTypeEntity = await _statusTypeRepository.GetAsync(x => x.StatusName == statusName);
        var statusType = StatusTypeFactory.Create(statusTypeEntity);
        return statusType ?? null!;
    }

    public async Task<StatusType> UppdateStatusTypeAsync(StatusTypeUpdateForm form)
    {
        var existingEntity = await _statusTypeRepository.GetAsync(x => x.Id == form.Id);
        if (existingEntity == null)
            return null!;

        existingEntity.StatusName = form.StatusName ?? existingEntity.StatusName;

        var result = await _statusTypeRepository.UpdateAsync(existingEntity);
        if (result == null)
            return null!;

        return StatusTypeFactory.Create(result);
    }

    public async Task<bool> DeleteStatusAsync(int id)
    {
        var result = await _statusTypeRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
