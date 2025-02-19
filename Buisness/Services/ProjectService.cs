using Buisness.Factories;
using Buisness.Interfaces;
using Buisness.Models;
using Data.Enteties;
using Data.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;

namespace Buisness.Services;

public class ProjectService(
    IProjectRepository projectRepository,
    ICustomerService customerService,
    IProductService productService,
    IStatusTypeService statusTypeService,
    IUserService userService) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly ICustomerService _customerService = customerService;
    private readonly IProductService _productService = productService;
    private readonly IStatusTypeService _statusTypeService = statusTypeService;
    private readonly IUserService _userService = userService;

    public async Task<Project> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var customer = await _customerService.GetCustomerByNameAsync(form.CustomerName);
        var product = await _productService.GetProductByNameAsync(form.ProductName);
        var status = await _statusTypeService.GetStatusByNameAsync(form.StatusName);
        var user = await _userService.GetUserByEmailAsync(form.Email);

        if (customer == null || product == null || status == null || user == null)
        {
            Console.WriteLine("Error: One or more related entities not found");
            return null!;
        }

        var newProject = new ProjectEntity
        {
            Titel = form.Titel,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            CustomerId = customer.Id,
            ProductId = product.Id,
            StatusId = status.Id,
            UserId = user.Id
        };

        var createdProject = await _projectRepository.CreateAsync(newProject);
        return ProjectFactory.Create(createdProject);
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(entity => new Project
        {
            Id = entity.Id,
            Titel = entity.Titel,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CustomerName = entity.Customer.CustomerName,
            ProductName = entity.Product.ProductName,
            Price = entity.Product.Price,
            StatusName = entity.Status.StatusName,
            FirstName = entity.User.FirstName,  
            LastName = entity.User.LastName, 
            Email = entity.User.Email
        });

        return projects ?? [];
    }

    public async Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        var project = ProjectFactory.Create(entity);
        return project ?? null!;
    }

    public async Task<Project> UpdateProjectAsync(ProjectUpdateForm form)
    {
        var existingProject = await _projectRepository.GetAsync(x => x.Id == form.Id);
        if (existingProject == null)
        {
            Console.WriteLine("Project not found.");
            return null!;
        }

        var customer = await _customerService.GetCustomerByNameAsync(form.CustomerName);
        var product = await _productService.GetProductByNameAsync(form.ProductName);
        var status = await _statusTypeService.GetStatusByNameAsync(form.StatusName);
        var user = await _userService.GetUserByEmailAsync(form.Email);

        existingProject.CustomerId = customer?.Id ?? existingProject.CustomerId;
        existingProject.ProductId = product?.Id ?? existingProject.ProductId;
        existingProject.StatusId = status?.Id ?? existingProject.StatusId;
        existingProject.UserId = user?.Id ?? existingProject.UserId;

        existingProject.Titel = form.Titel ?? existingProject.Titel;
        existingProject.Description = form.Description ?? existingProject.Description;
        existingProject.StartDate = form.StartDate ?? existingProject.StartDate;
        existingProject.EndDate = form.EndDate ?? existingProject.EndDate;

        var updatedProject = await _projectRepository.UpdateAsync(existingProject);
        return ProjectFactory.Create(updatedProject);
    }

    public async Task<bool> DeleteProjectAsync(int Id)
    {
        var result = await _projectRepository.DeleteAsync(x => x.Id == Id);
        return result;
    }

}
