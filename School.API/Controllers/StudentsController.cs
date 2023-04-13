using Microsoft.AspNetCore.Mvc;
using School.Business.Interface;

namespace School.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly ISchoolBusiness _schoolBusiness;
        public StudentsController(ILogger<StudentsController> logger, ISchoolBusiness schoolBusiness)
        {
            _logger = logger;
            _schoolBusiness = schoolBusiness;
        }

        [HttpGet]
        public IActionResult GetStudents(Guid ? id)
        {
            try
            {
                return Ok(_schoolBusiness.GetStudents(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
