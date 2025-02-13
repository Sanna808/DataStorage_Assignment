using Buisness.Models;

namespace Buisness.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(UserRegistrationForm form);

    Task<bool> DeleteProductAsync(int id);

    Task<IEnumerable<User>> GetAllUsersAsync();

    Task<User> GetUserByIdAsync(int id);

    Task<User> UppdateUserAsync(UserUpdateForm form);
}