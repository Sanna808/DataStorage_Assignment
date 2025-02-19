using Buisness.Factories;
using Buisness.Interfaces;
using Buisness.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Buisness.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> CreateUserAsync(UserRegistrationForm form)
    {
        var entity = await _userRepository.GetAsync(x => x.Email == form.Email);
        entity ??= await _userRepository.CreateAsync(UserFactory.Create(form));

        return UserFactory.Create(entity);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var enteties = await _userRepository.GetAllAsync();
        var users = enteties.Select(UserFactory.Create);
        return users ?? [];
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var userEntity = await _userRepository.GetAsync(x => x.Email == email);
        var user = UserFactory.Create(userEntity);
        return user ?? null!;
    }

    public async Task<User> UppdateUserAsync(UserUpdateForm form)
    {
        var existingEntity = await _userRepository.GetAsync(x => x.Id == form.Id);
        if (existingEntity == null)
            return null!;

        existingEntity.FirstName = form.FirstName ?? existingEntity.FirstName;
        existingEntity.LastName = form.LastName ?? existingEntity.LastName;
        existingEntity.Email = form.Email ?? existingEntity.Email;

        var result = await _userRepository.UpdateAsync(existingEntity);
        if (result == null)
            return null!;

        return UserFactory.Create(result);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var result = await _userRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
