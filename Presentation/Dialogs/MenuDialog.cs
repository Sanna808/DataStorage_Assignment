using Buisness.Interfaces;
using Buisness.Models;
using Data.Enteties;

namespace Presentation.Dialogs;

public class MenuDialog(ICustomerService customerService, IProductService productService, IProjectService projectService, IStatusTypeService statusTypeService, IUserService userService)
{
    private readonly ICustomerService _customerService = customerService;
    private readonly IProductService _productService = productService;
    private readonly IProjectService _projectService = projectService;
    private readonly IStatusTypeService _statusTypeService = statusTypeService;
    private readonly IUserService _userService = userService;

    public async Task MenuOptions()
    {
        while (true) 
        {
            Console.Clear();
            Console.WriteLine("---------- Menu ----------");
            Console.WriteLine("1. Create new Customer");
            Console.WriteLine("2. Show all available Customers");
            Console.WriteLine("3. Update Customer");
            Console.WriteLine("4. Detete Customer");
            Console.WriteLine("5. Create new Product");
            Console.WriteLine("6. Show all available Products");
            Console.WriteLine("7. Update Product");
            Console.WriteLine("8. Detete Product");
            Console.WriteLine("9. Create new Status Type");
            Console.WriteLine("10. Show all available Status Types");
            Console.WriteLine("11. Update Status Type");
            Console.WriteLine("12. Detete Status Type");
            Console.WriteLine("13. Create new User");
            Console.WriteLine("14. Show all available Users");
            Console.WriteLine("15. Update User");
            Console.WriteLine("16. Detete User");
            Console.WriteLine("17. Create new Project");
            Console.WriteLine("18. Show all Projects");
            Console.WriteLine("19. Update Project");
            Console.WriteLine("20. Detete Project");
            Console.WriteLine("21. Quit");
            Console.WriteLine("- Select your menu option: -");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateCustomerOption();
                    break;

                case "2":
                    await ShowAllCustomersOption();
                    break;

                case "3":
                    await UpdateCustomerOption();
                    break;

                case "4":
                    await DeleteCustomerOption();
                    break;

                case "5":
                    await CreateProductOption();
                    break;

                case "6":
                    await ShowAllProductsOption();
                    break;

                case "7":
                    await UpdateProductOption();
                    break;

                case "8":
                    await DeleteProductOption();
                    break;
                case "9":
                    await CreateStatusTypeOption();
                    break;

                case "10":
                    await ShowAllStatusTypesOption();
                    break;

                case "11":
                    await UpdateStatusTypeOption();
                    break;

                case "12":
                    await DeleteStatusTypeOption();
                    break;

                case "13":
                    await CreateUserOption();
                    break;

                case "14":
                    await ShowAllUsersOption();
                    break;

                case "15":
                    await UpdateUserOption();
                    break;

                case "16":
                    await DeleteUserOption();
                    break;

                case "17":
                    await CreateProjectOption();
                    break;

                case "18":
                    await ShowAllProjectsOption();
                    break;

                case "19":
                    await UpdateProjectOption();
                    break;

                case "20":
                    await DeleteProjectOption();
                    break;

                case "21":
                    await QuitOption();
                    break;

                default:
                    await InvalidOption();
                    break;

            }
        }
    }

    private async Task CreateCustomerOption()
    {
        Console.Clear();
        Console.WriteLine("- Create new Customer -");

        Console.Write("Customer name: ");
        var customerName = Console.ReadLine()!;

        var newCustomer = new CustomerRegistraitionForm { CustomerName = customerName };

        var result = await _customerService.CreateCustomerAsync(newCustomer);
        if (result != null)
        {
            Console.WriteLine("Customer was created successfully");
        }
        else
        {
            Console.WriteLine("Customer was not created");
        }
        Console.ReadKey();
    }

    private async Task ShowAllCustomersOption()
    {
        Console.Clear();
        Console.WriteLine("- Show all available Customers -");

        var customers = await _customerService.GetAllCustomersAsync();
        if (customers != null && customers.Any())
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"Id: {customer.Id} Name: {customer.CustomerName}");
            }
        }
        else
        {
            Console.WriteLine("No Customers found");
        }
        Console.ReadKey();
    }

    private async Task UpdateCustomerOption()
    {
        Console.Clear();
        Console.WriteLine("- Update Customer -");

        var existingCustomer = await ShowAvailableCustomersAndSelect();
        if (existingCustomer == null)
            return;

        Console.WriteLine($"Updating Customer: {existingCustomer.Id} {existingCustomer.CustomerName} ");

        Console.Write($"New Customer Name (Current: {existingCustomer.CustomerName}, leave empty to keep):");
        var customerName = Console.ReadLine()!;

        var updatedForm = new CustomerUpdateForm 
        {
            Id = existingCustomer.Id,
            CustomerName = string.IsNullOrWhiteSpace(customerName) ? existingCustomer.CustomerName : customerName
        };

        var result = await _customerService.UppdateCustomerAsync(updatedForm);
        if (result != null) 
        {
            Console.WriteLine("Customer was updated successfully");
        }
        else
        {
            Console.WriteLine("Customer was not updated");
        }
        Console.ReadKey();
    }

    private async Task DeleteCustomerOption()
    {
        Console.Clear();
        Console.WriteLine("- Delete Customer -");

        var existingCustomer = await ShowAvailableCustomersAndSelect();
        if (existingCustomer == null) 
            return;

        Console.WriteLine($"Are you sure you want to delete Customer: {existingCustomer.Id} {existingCustomer.CustomerName} ? (y/n): ");
        var option = Console.ReadLine();

        if (option?.ToLower() == "y")
        {
            var result = await _customerService.DeleteCustomerAsync(existingCustomer.Id);

            if (result)
            {
                Console.WriteLine("Customer was deleted successfully");
            }
            else
            {
                Console.WriteLine("Customer was not deleted");
            }
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Delete operation cancelled");
        }
        Console.ReadKey();
    }

    private async Task CreateProductOption()
    {
        Console.Clear();
        Console.WriteLine("- Create new Product -");

        Console.Write("Product name: ");
        var productName = Console.ReadLine()!;

        Console.Write("Price: ");
        decimal price = GetValidDecimal();

        var newProduct = new ProductRegistrationForm 
        {
            ProductName = productName,
            Price = price,
        };

        var result = await _productService.CreateProductAsync(newProduct);
        if (result != null)
        {
            Console.WriteLine("Product was created successfully");
        }
        else
        {
            Console.WriteLine("Product was not created");
        }
        Console.ReadKey();
    }

    private async Task ShowAllProductsOption()
    {
        Console.Clear();
        Console.WriteLine("- Show all available Products -");

        var products = await _productService.GetAllProductsAsync();
        if (products != null && products.Any())
        {
            foreach (var product in products)
            {
                Console.WriteLine($"Id: {product.Id} Name: {product.ProductName} Price: {product.Price}");
            }
        }
        else
        {
            Console.WriteLine("No Products found");
        }
        Console.ReadKey();
    }

    private async Task UpdateProductOption()
    {
        Console.Clear();
        Console.WriteLine("- Update Product -");

        var existingProduct = await ShowAvailableProductsAndSelect();
        if (existingProduct == null)
            return;

        Console.WriteLine($"Updating Product: {existingProduct.Id} {existingProduct.ProductName} ");

        Console.Write($"New Product Name (Current: {existingProduct.ProductName}, leave empty to keep):");
        var productName = Console.ReadLine()!;

        Console.Write($"New Price (Current: {existingProduct.Price}, leave empty to keep):");
        Decimal? price = GetNullableDecimal(existingProduct.Price);

        var updatedForm = new ProductUpdateForm 
        {
            Id = existingProduct.Id,
            ProductName = string.IsNullOrWhiteSpace(productName) ? existingProduct.ProductName : productName,
            Price = price,
        };

        var result = await _productService.UppdateProductAsync(updatedForm);
        if (result != null)
        {
            Console.WriteLine("Product was updated successfully");
        }
        else
        {
            Console.WriteLine("Product was not updated");
        }
        Console.ReadKey();
    }

    private async Task DeleteProductOption()
    {
        Console.Clear();
        Console.WriteLine("- Delete Product -");

        var existingProduct = await ShowAvailableProductsAndSelect();
        if (existingProduct == null)
            return;

        Console.WriteLine($"Are you sure you want to delete Product: {existingProduct.Id} {existingProduct.ProductName} {existingProduct.Price} ? (y/n): ");
        var option = Console.ReadLine();

        if (option?.ToLower() == "y")
        {
            var result = await _productService.DeleteProductAsync(existingProduct.Id);
   
            if (result)
            {
                Console.WriteLine("Product was deleted successfully");
            }
            else
            {
                Console.WriteLine("Product was not deleted");
            }
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Delete operation cancelled");
        }
        Console.ReadKey();
    }

    private async Task CreateStatusTypeOption()
    {
        Console.Clear();
        Console.WriteLine("- Create new Status Type -");

        Console.Write("Status Type: ");
        var statusName = Console.ReadLine()!;

        var newStatusType = new StatusTypeRegistrationForm { StatusName = statusName };

        var result = await _statusTypeService.CreateStatusTypeAsync(newStatusType);
        if (result != null)
        {
            Console.WriteLine("Status Type was created successfully");
        }
        else
        {
            Console.WriteLine("Status Type was not created");
        }
        Console.ReadKey();
    }

    private async Task ShowAllStatusTypesOption()
    {
        Console.Clear();
        Console.WriteLine("- Show all available Status Types -");

        var statusTypes = await _statusTypeService.GetAllStatusTypesAsync();
        if (statusTypes != null && statusTypes.Any())
        {
            foreach (var statusType in statusTypes)
            {
                Console.WriteLine($"Id: {statusType.Id} Name: {statusType.StatusName}");
            }
        }
        else
        {
            Console.WriteLine("No Status Types found");
        }
        Console.ReadKey();
    }

    private async Task UpdateStatusTypeOption()
    {
        Console.Clear();
        Console.WriteLine("- Update Status Type -");

        var existingStatusType = await ShowAvailableStatusTypesAndSelect();
        if (existingStatusType == null)
            return;

        Console.WriteLine($"Updating Status Type: {existingStatusType.Id} {existingStatusType.StatusName} ");

        Console.Write($"New Status Name (Current: {existingStatusType.StatusName}, leave empty to keep):");
        var statusName = Console.ReadLine()!;

        var updatedForm = new StatusTypeUpdateForm 
        { 
            Id = existingStatusType.Id,
            StatusName = string.IsNullOrWhiteSpace(statusName) ? existingStatusType.StatusName : statusName
        };

        var result = await _statusTypeService.UppdateStatusTypeAsync(updatedForm);
        if (result != null)
        {
            Console.WriteLine("Status Type was updated successfully");
        }
        else
        {
            Console.WriteLine("Status Type was not updated");
        }
        Console.ReadKey();
    }

    private async Task DeleteStatusTypeOption()
    {
        Console.Clear();
        Console.WriteLine("- Delete Status Type -");

        var existingStatusType = await ShowAvailableStatusTypesAndSelect();
        if (existingStatusType == null)
            return;

        Console.WriteLine($"Are you sure you want to delete Status Type: {existingStatusType.Id} {existingStatusType.StatusName} ? (y/n): ");
        var option = Console.ReadLine();

        if (option?.ToLower() == "y")
        {
            var result = await _statusTypeService.DeleteStatusAsync(existingStatusType.Id);

            if (result)
            {
                Console.WriteLine("Status Type was deleted successfully");
            }
            else
            {
                Console.WriteLine("Status Type was not deleted");
            }
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Delete operation cancelled");
        }
        Console.ReadKey();
    }

    private async Task CreateUserOption()
    {
        Console.Clear();
        Console.WriteLine("- Create new User -");

        Console.Write("First name: ");
        var firstName = Console.ReadLine()!;

        Console.Write("Last name: ");
        var lastName = Console.ReadLine()!;

        Console.Write("Email: ");
        var email = Console.ReadLine()!;

        var newUser = new UserRegistrationForm 
        { 
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };

        var result = await _userService.CreateUserAsync(newUser);
        if (result != null)
        {
            Console.WriteLine("User was created successfully");
        }
        else
        {
            Console.WriteLine("User was not created");
        }
        Console.ReadKey();
    }
    private async Task ShowAllUsersOption()
    {
        Console.Clear();
        Console.WriteLine("- Show all available Users -");

        var users = await _userService.GetAllUsersAsync();
        if (users != null && users.Any())
        {
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.Id} Name: {user.FirstName} {user.LastName} Email: {user.Email}");
            }
        }
        else
        {
            Console.WriteLine("No Users found");
        }
        Console.ReadKey();
    }

    private async Task UpdateUserOption()
    {
        Console.Clear();
        Console.WriteLine("- Update User -");

        var existingUser = await ShowAvailableUsersAndSelect();
        if (existingUser == null)
            return;

        Console.WriteLine($"Updating User: Id: {existingUser.Id} Name: {existingUser.FirstName} {existingUser.LastName} Email: {existingUser.Email} ");

        Console.Write($"New First name (Current: {existingUser.FirstName}, leave empty to keep):");
        var firstName = Console.ReadLine()!;

        Console.Write($"New Last name (Current: {existingUser.LastName}, leave empty to keep):");
        var lastName = Console.ReadLine()!;

        Console.Write($"New Email (Current: {existingUser.Email}, leave empty to keep):");
        var email = Console.ReadLine()!;

        var updatedForm = new UserUpdateForm 
        { 
            Id = existingUser.Id,
            FirstName = string.IsNullOrWhiteSpace(firstName) ? existingUser.FirstName : firstName,
            LastName = string.IsNullOrWhiteSpace(lastName) ? existingUser.LastName : lastName,
            Email = string.IsNullOrWhiteSpace(email) ? existingUser.Email : email
        };

        var result = await _userService.UppdateUserAsync(updatedForm);
        if (result != null)
        {
            Console.WriteLine("User was updated successfully");
        }
        else
        {
            Console.WriteLine("User was not updated");
        }
        Console.ReadKey();
    }

    private async Task DeleteUserOption()
    {
        Console.Clear();
        Console.WriteLine("- Delete User -");

        var existingUser = await ShowAvailableUsersAndSelect();
        if (existingUser == null)
            return;

        Console.WriteLine($"Are you sure you want to delete User: {existingUser.Id} {existingUser.FirstName} {existingUser.LastName} {existingUser.Email} ? (y/n): ");
        var option = Console.ReadLine();

        if (option?.ToLower() == "y")
        {
            var result = await _userService.DeleteUserAsync(existingUser.Id);
            if (result)
            {
                Console.WriteLine("User was deleted successfully");
            }
            else
            {
                Console.WriteLine("User was not deleted");
            }
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Delete operation cancelled");
        }
        Console.ReadKey();
    }

    private async Task CreateProjectOption()
    {
        Console.Clear();
        Console.WriteLine("- Create new Project -");

        Console.Write("Project title: ");
        var title = Console.ReadLine()!;

        Console.Write("Description (optional): ");
        var description = Console.ReadLine();

        Console.Write("Start Date (YYYY-MM-DD): ");
        var startDate = GetValidDate();

        Console.Write("End Date (YYYY-MM-DD, leave empty if unknown): ");
        var endDate = GetNullableDate(null);

        Console.WriteLine("-------------------------------");

        var customer = await ShowAvailableCustomersAndSelect();
        if (customer == null) 
            return;

        Console.WriteLine("-------------------------------");

        var product = await ShowAvailableProductsAndSelect();
        if (product == null) 
            return;

        Console.WriteLine("-------------------------------");

        var status = await ShowAvailableStatusTypesAndSelect();
        if (status == null) 
            return;

        Console.WriteLine("-------------------------------");

        var user = await ShowAvailableUsersAndSelect();
        if (user == null) 
            return;

        var newProject = new ProjectRegistrationForm
        {
            Titel = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            CustomerName = customer.CustomerName,
            ProductName = product.ProductName,
            StatusName = status.StatusName,
            Email = user.Email
        };

        var result = await _projectService.CreateProjectAsync(newProject);
        if (result != null)
        {
            Console.WriteLine("Project was created successfully.");
        }
        else
        {
            Console.WriteLine("Project was not created.");
        }
        Console.ReadKey();
    }

    private async Task UpdateProjectOption()
    {
        Console.Clear();
        Console.WriteLine("- Update Project -");

        var existingProject = await ShowAvailableProjectsAndSelect();
        if (existingProject == null)
            return;

        Console.WriteLine($"Updating Project: {existingProject.Id} {existingProject.Titel}");

        Console.Write($"New Title (Current: {existingProject.Titel}, leave empty to keep): ");
        var title = Console.ReadLine();

        Console.Write($"New Description (Current: {existingProject.Description}, leave empty to keep): ");
        var description = Console.ReadLine();

        Console.Write($"New Start Date (YYYY-MM-DD, Current: {existingProject.StartDate?.ToShortDateString()}, leave empty to keep): ");
        var startDate = GetNullableDate(existingProject.StartDate);

        Console.Write($"New End Date (YYYY-MM-DD, Current: {existingProject.EndDate?.ToShortDateString()}, leave empty to keep): ");
        var endDate = GetNullableDate(existingProject.EndDate);

        Console.WriteLine("-------------------------------");

        Console.Write("Do you want to update Customer? (y/n): ");
        var updateCustomer = Console.ReadLine();
        var customerName = existingProject.CustomerName;
        if (updateCustomer?.ToLower() == "y")
        {
            var customer = await ShowAvailableCustomersAndSelect();
            if (customer != null) customerName = customer.CustomerName;
        }

        Console.WriteLine("-------------------------------");

        Console.Write("Do you want to update Product? (y/n): ");
        var updateProduct = Console.ReadLine();
        var productName = existingProject.ProductName;
        if (updateProduct?.ToLower() == "y")
        {
            var product = await ShowAvailableProductsAndSelect();
            if (product != null) productName = product.ProductName;
        }

        Console.WriteLine("-------------------------------");

        Console.Write("Do you want to update Status? (y/n): ");
        var updateStatus = Console.ReadLine();
        var statusName = existingProject.StatusName;
        if (updateStatus?.ToLower() == "y")
        {
            var status = await ShowAvailableStatusTypesAndSelect();
            if (status != null) statusName = status.StatusName;
        }

        Console.WriteLine("-------------------------------");

        Console.Write("Do you want to update Responsible User? (y/n): ");
        var updateUser = Console.ReadLine();
        var email = existingProject.Email;
        if (updateUser?.ToLower() == "y")
        {
            var user = await ShowAvailableUsersAndSelect();
            if (user != null) email = user.Email;
        }

        var updatedProject = new ProjectUpdateForm
        {
            Id = existingProject.Id,
            Titel = string.IsNullOrWhiteSpace(title) ? existingProject.Titel : title,
            Description = string.IsNullOrWhiteSpace(description) ? existingProject.Description : description,
            StartDate = startDate,
            EndDate = endDate,
            CustomerName = customerName,
            ProductName = productName,
            StatusName = statusName,
            Email = email
        };

        var result = await _projectService.UpdateProjectAsync(updatedProject);
        if (result != null)
        {
            Console.WriteLine("Project was updated successfully.");
        }
        else
        {
            Console.WriteLine("Project was not updated.");
        }
        Console.ReadKey();
    }

    private async Task ShowAllProjectsOption()
    {
        Console.Clear();
        Console.WriteLine("- Show all Projects -");

        var projects = await _projectService.GetAllProjectsAsync();
        if (projects != null && projects.Any())
        {
            foreach (var project in projects)
            {
                Console.WriteLine($"Id: {project.Id}");
                Console.WriteLine($"Title: {project.Titel}");
                Console.WriteLine($"Description: {project.Description}");
                Console.WriteLine($"Start Date: {project.StartDate?.ToShortDateString()}");
                Console.WriteLine($"End Date: {project.EndDate?.ToShortDateString()}");
                Console.WriteLine($"Customer: {project.CustomerName}");
                Console.WriteLine($"Product: {project.ProductName} (Price: {project.Price} kr)");
                Console.WriteLine($"Status: {project.StatusName}");
                Console.WriteLine($"Responsible User: {project.FirstName} {project.LastName} ({project.Email})");
                Console.WriteLine("----------------------------------");
            }
        }
        else
        {
            Console.WriteLine("No projects found.");
        }
        Console.ReadKey();
    }
    private async Task DeleteProjectOption()
    {
        Console.Clear();
        Console.WriteLine("- Delete Project -");

        var existingProject = await ShowAvailableProjectsAndSelect();
        if (existingProject == null)
            return;

        Console.WriteLine($"Are you sure you want to delete Project: {existingProject.Id} {existingProject.Titel}? (y/n): ");
        var option = Console.ReadLine();

        if (option?.ToLower() == "y")
        {
            var result = await _projectService.DeleteProjectAsync(existingProject.Id);
            if (result)
            {
                Console.WriteLine("Project was deleted successfully.");
            }
            else
            {
                Console.WriteLine("Project was not deleted.");
            }
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Delete operation cancelled.");
        }
        Console.ReadKey();
    }

    static DateTime GetValidDate()
    {
        DateTime date;
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.Write("Incorrect format, try again (YYYY-MM-DD): ");
        }
        return date;
    }

    static DateTime? GetNullableDate(DateTime? currentDate)
    {
        string input = Console.ReadLine()!;
        return string.IsNullOrWhiteSpace(input) ? currentDate : DateTime.Parse(input);
    }


    static decimal GetValidDecimal()
    {
        decimal value;
        while (!decimal.TryParse(Console.ReadLine(), out value) || value <= 0)
        {
            Console.Write("Error: Submit a valid price (> 0): ");
        }
        return value;
    }

    static decimal? GetNullableDecimal(decimal? currentValue)
    {
        string input = Console.ReadLine()!;
        return string.IsNullOrWhiteSpace(input) ? currentValue : decimal.Parse(input);
    }

    private async Task<Customer?> ShowAvailableCustomersAndSelect()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        if (customers == null || !customers.Any())
        {
            Console.WriteLine("No Customers found to update");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine("Available Customers: ");
        foreach (var customer in customers)
        {
            Console.WriteLine($"Customer Id: {customer.Id}");
            Console.WriteLine($"Name: {customer.CustomerName}");
            Console.WriteLine("----------------------------------");
        }

        Console.Write("Enter Customer Id to update: ");
        if (!int.TryParse(Console.ReadLine(), out var customerId))
        {
            Console.WriteLine("Invalid Id");
            Console.ReadKey();
            return null;
        }

        var existingCustomer = customers.FirstOrDefault(x => x.Id == customerId);
        if (existingCustomer == null)
        {
            Console.WriteLine("Customer not found");
            Console.ReadKey();
            return null;
        }
        return existingCustomer;
    }

    private async Task<Product?> ShowAvailableProductsAndSelect()
    {
        var products = await _productService.GetAllProductsAsync();
        if (products == null || !products.Any())
        {
            Console.WriteLine("No Products found to update");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine("Available Products: ");
        foreach (var product in products)
        {
            Console.WriteLine($"Product Id: {product.Id}");
            Console.WriteLine($"Name: {product.ProductName}");
            Console.WriteLine($"Price: {product.Price}");
            Console.WriteLine("----------------------------------");
        }

        Console.Write("Enter Product Id to update: ");
        if (!int.TryParse(Console.ReadLine(), out var productId))
        {
            Console.WriteLine("Invalid Id");
            Console.ReadKey();
            return null;
        }

        var existingProduct = products.FirstOrDefault(x => x.Id == productId);
        if (existingProduct == null)
        {
            Console.WriteLine("Product not found");
            Console.ReadKey();
            return null;
        }
        return existingProduct;
    }

    private async Task<StatusType?> ShowAvailableStatusTypesAndSelect()
    {
        var statusTypes = await _statusTypeService.GetAllStatusTypesAsync();
        if (statusTypes == null || !statusTypes.Any())
        {
            Console.WriteLine("No StatusTypes found to update");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine("Available Status Types: ");
        foreach (var statusType in statusTypes)
        {
            Console.WriteLine($"Status Type Id: {statusType.Id}");
            Console.WriteLine($"Status name: {statusType.StatusName}");
            Console.WriteLine("----------------------------------");
        }

        Console.Write("Enter Status Type Id to update: ");
        if (!int.TryParse(Console.ReadLine(), out var statusTypeId))
        {
            Console.WriteLine("Invalid Id");
            Console.ReadKey();
            return null;
        }

        var existingStatusType = statusTypes.FirstOrDefault(x => x.Id == statusTypeId);
        if (existingStatusType == null)
        {
            Console.WriteLine("Status Type not found");
            Console.ReadKey();
            return null;
        }
        return existingStatusType;
    }

    private async Task<User?> ShowAvailableUsersAndSelect()
    {
        var users = await _userService.GetAllUsersAsync();
        if (users == null || !users.Any())
        {
            Console.WriteLine("No Users found to update");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine("Available Users: ");
        foreach (var user in users)
        {
            Console.WriteLine($"User Id: {user.Id}");
            Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine("----------------------------------");
        }

        Console.Write("Enter User Id to update: ");
        if (!int.TryParse(Console.ReadLine(), out var userId))
        {
            Console.WriteLine("Invalid Id");
            Console.ReadKey();
            return null;
        }

        var existingUser = users.FirstOrDefault(x => x.Id == userId);
        if (existingUser == null)
        {
            Console.WriteLine("User not found");
            Console.ReadKey();
            return null;
        }
        return existingUser;
    }

    private async Task<Project?> ShowAvailableProjectsAndSelect()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        if (projects == null || !projects.Any())
        {
            Console.WriteLine("No Projects found to update");
            Console.ReadKey();
            return null;
        }

        Console.WriteLine("Available Projects: ");
        foreach (var project in projects)
        {
            Console.WriteLine($"Project Id: {project.Id}");
            Console.WriteLine($"Titel: {project.Titel}");
            Console.WriteLine($"Start Date: {project.StartDate}");
            Console.WriteLine($"End Date{project.EndDate}");
            Console.WriteLine($"Status: {project.StatusName}");
            Console.WriteLine("----------------------------------");
        }

        Console.Write("Enter Project Id to update: ");
        if (!int.TryParse(Console.ReadLine(), out var customerId))
        {
            Console.WriteLine("Invalid Id");
            Console.ReadKey();
            return null;
        }

        var existingProject = projects.FirstOrDefault(x => x.Id == customerId);
        if (existingProject == null)
        {
            Console.WriteLine("Project not found");
            Console.ReadKey();
            return null;
        }
        return existingProject;
    }

    private async Task QuitOption()
    {
        Console.Clear();
        Console.Write("Do you want to quit this application (y/n): ");

        var option = await Task.Run(() => Console.ReadLine()!);

        if (option.Equals("y", StringComparison.CurrentCultureIgnoreCase))
        {
            Environment.Exit(0);
        }
    }

    private async Task InvalidOption()
    {
        Console.Clear();
        Console.WriteLine("You must enter a valid option.");

        await Task.Run(() => Console.ReadKey());
    }
}
