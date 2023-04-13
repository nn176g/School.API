using Microsoft.Extensions.Logging;
using School.Data.Models;
using School.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace School.Repository.SchoolRepository
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly ILogger _logger;
        private readonly OigaDbContext _dbContext;
        public SchoolRepository(OigaDbContext dbContext, ILogger<SchoolRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Estudiantes
        public IEnumerable<Student> GetStudents(Guid? id)
        {
            _logger.LogInformation("Obteniendo los estudientes");
            List<Student> result;
            if(id == null)
            {
                result = _dbContext.Students
                    .Include(student => student.CourseStudents)
                        .ThenInclude(courseStudent => courseStudent.Evaluations).ToList();
            }
            else
            {
                result = _dbContext.Students
                    .Include(student => student.CourseStudents)
                        .ThenInclude(courseStudent => courseStudent.Evaluations)
                    .Where(x => x.Id == id).ToList();
            }
            if(result != null)
            {
                _logger.LogInformation("Obtener estudiantes funciona correctamente");
                return result;
            }
            return result;
        }
        #endregion

        #region Cursos

        public IEnumerable<Course> GetCourses(Guid? id)
        {
            _logger.LogInformation("Obteniendo los cursos");
            List<Course> result;
            if (id == null)
            {
                result = _dbContext.Courses
                    .Include(courses => courses.CourseStudents)
                        .ThenInclude(courseStudent => courseStudent.Evaluations).ToList();
            }
            else
            {
                result = _dbContext.Courses
                    .Include(courses => courses.CourseStudents)
                        .ThenInclude(courseStudent => courseStudent.Evaluations)
                    .Where(x => x.Id == id).ToList();
            }
            if (result != null)
            {
                _logger.LogInformation("Obtener cursos correctamente");
                return result;
            }
            return result;
        }

        #endregion

        #region "Evaluaciones"
        public IEnumerable<Evaluation> GetEvaluationsForCourse(Guid id, int? starts)
        {
            _logger.LogInformation("Obteniendo las evaluaciones para un curso en especial");
            //List<Student> result;
            List<Evaluation> result;


            if (starts == null)
            {
                result = _dbContext.CourseStudents
                    .Where(x => x.CourseId == id)
                    .SelectMany(x => x.Evaluations)
                    .Select(y => y).ToList();
            }
            else
            {
                result = _dbContext.CourseStudents
                    .Where(x => x.CourseId == id)
                    .SelectMany(x => x.Evaluations)
                    .Select(y => y)
                    .Where(y => y.Stars == starts).ToList();
            }
            if (result != null)
            {
                _logger.LogInformation("Obtener las evaluaciones para un curso funciona correctamente");
                return result;
            }
            return result;
        }

        private Evaluation GetEvaluation(Guid id)
        {
            _logger.LogInformation("Obteniendo la evaluacion para un estudiante");
            var result = _dbContext.CourseStudents
                .Where(x => x.CourseId == id)
                .SelectMany(x => x.Evaluations)
                .Select(y => y).FirstOrDefault();
            if (result != null)
            {
                _logger.LogInformation("Obtener evaluacion para un estudiante funciona correctamente");
                return result;
            }
            return result;
        }

        public async Task<Evaluation> InsertEvaluationToStudent(Evaluation evaluation)
        {
            _logger.LogInformation("Creando Nueva Evaluacion para un estudiante");
            var tempEvaluation = NormalizeEntity(evaluation);
            _dbContext.Evaluations.Add(tempEvaluation);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Evaluacion creada correctamente");
            return tempEvaluation;
        }

        public async Task<Evaluation> UpdateEvaluationToStudent(Evaluation evaluation)
        {
            _logger.LogInformation("Actualizando una Evaluacion para un estudiante");
            var tempEvaluation = NormalizeEntity(evaluation);
            tempEvaluation.Id = evaluation.Id;
            _dbContext.Evaluations.Update(tempEvaluation);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Evaluacion actaulizada correctamente");
            return tempEvaluation;
        }

        public async Task DeleteEvaluationToStudent(Evaluation evaluation)
        {
            _logger.LogInformation("Eliminando una Evaluacion para un estudiante");
            var tempEvaluation = GetEvaluation(evaluation.Id);
            if (tempEvaluation != null)
            {
                _dbContext.Evaluations.Remove(tempEvaluation);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Evaluacion eliminada correctamente");
            }
            else
            {
                _logger.LogInformation("Evaluacion no encontrada para eliminar");
            }
        }
        #endregion

        private Evaluation NormalizeEntity(Evaluation evaluation)
        {
            Evaluation tempEvaluation = new Evaluation
            {
                Id = evaluation.Id,
                CourseStudent = evaluation.CourseStudent,
                Stars = evaluation.Stars,
                Description = evaluation.Description,
                CreationDate = DateTime.Now
            };
            return tempEvaluation;
        }

    }
}
