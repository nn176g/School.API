using Microsoft.AspNetCore.Mvc;
using School.Business.Interface;

namespace School.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly ISchoolBusiness _schoolBusiness;
        public CourseController(ILogger<StudentsController> logger, ISchoolBusiness schoolBusiness)
        {
            _logger = logger;
            _schoolBusiness = schoolBusiness;
        }

        [HttpGet("GetCourses")]
        public IActionResult GetCourses(Guid? id)
        {
            try
            {
                return Ok(_schoolBusiness.GetCourses(id));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
