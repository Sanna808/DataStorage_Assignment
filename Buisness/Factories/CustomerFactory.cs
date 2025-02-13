using Buisness.Models;
using Data.Enteties;

namespace Buisness.Factories;

public static class CustomerFactory
{
    public static CustomerEntity? Create(CustomerRegistraitionForm form) => form == null ? null : new()
    {
        CustomerName = form.CustomerName,
    };

    public static Customer? Create(CustomerEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName,
    };
}
