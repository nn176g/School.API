using Microsoft.AspNetCore.Mvc;
using School.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Business.Interface
{
    public interface ISchoolBusiness
    {
        IEnumerable<Student> GetStudents(Guid ? id);
        IEnumerable<Evaluation> GetEvaluationsForCourse(Guid id, int? starts);
        //Evaluation GetEvaluation(Guid id);
        Task<Evaluation> AddEvalutionToStudent(Evaluation model);
        Task<Evaluation> UpdateEvaluationToStudent(Evaluation model);
        Task<Evaluation> DeleteEvaluationToStudent(Evaluation model);

        IEnumerable<Course> GetCourses(Guid? id);
    }
}
