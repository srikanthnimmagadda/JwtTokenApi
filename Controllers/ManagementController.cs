using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Helpers.UserRole.Manager)]
    public class ManagementController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to Management Controller");
        }
    }
}
