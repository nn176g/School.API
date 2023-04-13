using System;
using System.Collections.Generic;

namespace School.Data.Models;

public partial class Student
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public DateTime? CreationDate { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<CourseStudent> CourseStudents { get; } = new List<CourseStudent>();
}
