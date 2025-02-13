using Buisness.Factories;
using Buisness.Interfaces;
using Buisness.Models;
using Data.Interfaces;
using Data.Repositories;

namespace Buisness.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Customer> CreateCustomerAsync(CustomerRegistraitionForm form)
    {
        var entity = await _customerRepository.GetAsync(x => x.CustomerName == form.CustomerName);
        entity ??= await _customerRepository.CreateAsync(CustomerFactory.Create(form));

        return CustomerFactory.Create(entity);
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var enteties = await _customerRepository.GetAllAsync();
        var customers = enteties.Select(CustomerFactory.Create);
        return customers ?? [];
    }

    public async Task<Customer> GetCustomerByNameAsync(string customerName)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        var customer = CustomerFactory.Create(customerEntity);
        return customer ?? null!;
    }

    public async Task<Customer> UppdateCustomerAsync(CustomerUpdateForm form)
    {
        var existingEntity = await _customerRepository.GetAsync(x => x.CustomerName == form.CustomerName);
        if (existingEntity == null)
            return null!;

        existingEntity.CustomerName = form.CustomerName ?? existingEntity.CustomerName;

        var result = await _customerRepository.UpdateAsync(x => x.Id == form.Id, existingEntity);
        if (result == null)
            return null!;

        return CustomerFactory.Create(result);
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var result = await _customerRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
