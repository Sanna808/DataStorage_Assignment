using Buisness.Models;
using Data.Enteties;

namespace Buisness.Factories;

public static class CustomerFactory
{
    public static CustomerRegistraitionForm Create() => new();

    public static CustomerEntity Create(CustomerRegistraitionForm form) => new()
    {
        CustomerName = form.CustomerName,
    };

    public static Customer Create(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName,
    };

    public static CustomerUpdateForm Create(Customer customer) => new()
    {
        Id = customer.Id,
        CustomerName = customer.CustomerName,
    };

    public static CustomerEntity Create(CustomerUpdateForm form) => new()
    {
        Id = form.Id,
        CustomerName = form.CustomerName,
    };
}
