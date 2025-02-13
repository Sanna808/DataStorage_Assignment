﻿using System.ComponentModel.DataAnnotations;

namespace Data.Enteties;

public class UserEntity
{
    [Key]

    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];
}
