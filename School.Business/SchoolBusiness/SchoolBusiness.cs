using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using School.Business.Interface;
using School.Data.Models;
using School.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Business.SchoolBusiness
{
    public class SchoolBusiness : ISchoolBusiness
    {
        private readonly ISchoolRepository _schoolRepository;
        public SchoolBusiness(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public IEnumerable<Student> GetStudents(Guid ? id)
        {
            return _schoolRepository.GetStudents(id);
        }

        public IEnumerable<Evaluation> GetEvaluationsForCourse(Guid id, int? starts)
        {
            return _schoolRepository.GetEvaluationsForCourse(id, starts);
        }

        //public IEnumerable<Evaluation> GetEvaluation(Guid id)
        //{
        //    return _schoolRepository.GetEvaluation(id);
        //}
        public async Task<Evaluation> AddEvalutionToStudent(Evaluation model)
        {
            return await _schoolRepository.InsertEvaluationToStudent(model);
        }
        public async Task<Evaluation> UpdateEvaluationToStudent(Evaluation model)
        {
            return await _schoolRepository.UpdateEvaluationToStudent(model);
        }
        public async Task<Evaluation> DeleteEvaluationToStudent(Evaluation model)
        {
            await _schoolRepository.DeleteEvaluationToStudent(model);
            return new Evaluation();
        }



        public IEnumerable<Course> GetCourses(Guid? id)
        {
            return _schoolRepository.GetCourses(id);
        }
    }
}
