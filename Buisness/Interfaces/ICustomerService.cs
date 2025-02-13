using Buisness.Models;

namespace Buisness.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomerAsync(CustomerRegistraitionForm form);
        Task<bool> DeleteCustomerAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByNameAsync(string customerName);
        Task<Customer> UppdateCustomerAsync(CustomerUpdateForm form);
    }
}