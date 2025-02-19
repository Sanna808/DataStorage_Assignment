using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buisness.Models;

public class Project
{
    public int Id { get; set; }

    public string Titel { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string CustomerName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal? Price { get; set; }

    public string StatusName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!; 

    public string Email { get; set; } = null!;

}
