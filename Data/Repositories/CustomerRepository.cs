using Data.Contexts;
using Data.Enteties;
using Data.Interfaces;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context) , ICustomerRepository
{
    private readonly DataContext _context = context;
}