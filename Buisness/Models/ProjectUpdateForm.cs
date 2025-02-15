﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Buisness.Models;

public class ProjectUpdateForm
{
    public int Id { get; set; }

    public string Titel { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "date")]

    public DateTime? StartDate { get; set; }

    [Column(TypeName = "date")]

    public DateTime? EndDate { get; set; }
}
