using School.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository.Interface
{
    public interface ISchoolRepository
    {
        IEnumerable<Student> GetStudents(Guid? id);
        IEnumerable<Evaluation> GetEvaluationsForCourse(Guid id, int? starts);
        //Evaluation GetEvaluation(Guid id);
        Task<Evaluation> InsertEvaluationToStudent(Evaluation evaluation);
        Task<Evaluation> UpdateEvaluationToStudent(Evaluation evaluation);
        Task DeleteEvaluationToStudent(Evaluation evaluation);

        IEnumerable<Course> GetCourses(Guid? id);
    }
}
