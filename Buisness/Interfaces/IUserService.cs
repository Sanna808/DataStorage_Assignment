﻿using Buisness.Models;

namespace Buisness.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(UserRegistrationForm form);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> UppdateUserAsync(UserUpdateForm form);
    }
}