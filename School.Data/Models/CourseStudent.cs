using System;
using System.Collections.Generic;

namespace School.Data.Models;

public partial class CourseStudent
{
    public Guid Id { get; set; }

    public Guid? CourseId { get; set; }

    public Guid? StudentId { get; set; }

    public int? Grade { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Evaluation> Evaluations { get; } = new List<Evaluation>();

    public virtual Student? Student { get; set; }
}
