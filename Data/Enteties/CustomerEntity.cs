using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class CustomerEntity
{
    [Key]

    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
