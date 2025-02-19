using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buisness.Models;

public class ProjectRegistrationForm
{
    [Required]
    public string Titel { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "date")]

    public DateTime? StartDate { get; set; }

    [Column(TypeName = "date")]

    public DateTime? EndDate { get; set; }

    [Required]
    public string CustomerName { get; set; } = null!;

    [Required]
    public string ProductName { get; set; } = null!;

    [Required]
    public string StatusName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

}
