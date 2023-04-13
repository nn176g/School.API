using Microsoft.AspNetCore.Mvc;
using School.Business.Interface;
using School.Data.Models;

namespace School.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationController : Controller
    {
        private readonly ILogger<EvaluationController> _logger;
        private readonly ISchoolBusiness _schoolBusiness;

        public EvaluationController(ILogger<EvaluationController> logger, ISchoolBusiness schoolBusiness)
        {
            _logger = logger;
            _schoolBusiness = schoolBusiness;
        }

        [HttpGet("GetEvaluationForCourse")]
        public IActionResult GetEvaluationForCourse(Guid idCourse, int? starts)
        {
            try
            {
                return Ok(_schoolBusiness.GetEvaluationsForCourse(idCourse, starts));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPost("AddEvaluationToStudent")]
        public async Task<IActionResult> AddEvalutionToStudent(Evaluation model)
        {
            try
            {
                return Ok(await _schoolBusiness.AddEvalutionToStudent(model));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpPut("UpdateEvalutionToStudent")]
        public async Task<IActionResult> UpdateEvalutionToStudent(Evaluation model)
        {
            try
            {
                return Ok(await _schoolBusiness.UpdateEvaluationToStudent(model));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        [HttpDelete("DeleteEvalutionToStudent")]
        public async Task<IActionResult> DeleteEvalutionToStudent(Evaluation model)
        {
            try
            {
                return Ok(await _schoolBusiness.DeleteEvaluationToStudent(model));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
