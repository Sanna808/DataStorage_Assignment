using Buisness.Factories;
using Buisness.Models;
using Data.Repositories;

namespace Buisness.Services;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;

    public async Task<bool> CreateCustomerAsync(CustomerRegistraitionForm form) 
    {
        bool exists = await _customerRepository.AlredyExistsAsync(c => c.CustomerName == form.CustomerName);
        
        if (exists)
        {
            return false;
        } 

        var customerEntity = CustomerFactory.Create(form);
        await _customerRepository.CreateAsync(customerEntity!);
        return true;
    }

    public async Task<IEnumerable<Customer?>> GetAllCustomersAsync() 
    {
        var customerEnteties = await _customerRepository.GetAllAsync();
        return customerEnteties.Select(CustomerFactory.Create);
    }

    public async Task<Customer?> GetCustomerByNameAsync(string customerName) 
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        return CustomerFactory.Create(customerEntity!); 
    }

    public async Task<Customer?> GetCustomerByIdAsync(int Id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == Id);
        return CustomerFactory.Create(customerEntity!);
    }

    public async Task<Customer?> UppdateCustomerAsync(string customerName, Customer customer) 
    {
        var existingCustomer = await _customerRepository.GetAsync(x => x.CustomerName == customerName);
        if (existingCustomer == null)
            return null;

        existingCustomer.CustomerName = string.IsNullOrWhiteSpace(customer.CustomerName) ? existingCustomer.CustomerName : customer.CustomerName;

        var result = await _customerRepository.UpdateAsync(x => x.Id == Id, existingCustomer);
        return result ? CustomerFactory.Create(existingCustomer) : null;

    }

    public async Task<bool> DeleteCustomerAsync(int id) { }
}
