using Buisness.Interfaces;
using Buisness.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Dialogs;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\DataStorage_Assignment\\Data\\Databases\\local_database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"))
    .AddScoped<ICustomerRepository, CustomerRepository>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IProjectRepository, ProjectRepository>()
    .AddScoped<IStatusTypeRepository, StatusTypeRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IProjectService, ProjectService>()
    .AddScoped<IStatusTypeService, StatusTypeService>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<MenuDialog>()
    .BuildServiceProvider();

var projectDialogs = services.GetRequiredService<MenuDialog>();
await projectDialogs.MenuOptions();
