using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Helpers.UserRole.Student)]
    public class StudentController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to Student Controller");
        }
    }
}
