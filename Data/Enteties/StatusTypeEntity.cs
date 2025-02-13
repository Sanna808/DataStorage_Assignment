using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class StatusTypeEntity
{
    [Key]

    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}