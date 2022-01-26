using JwtTokenApi.Data;
using JwtTokenApi.Domain;
using JwtTokenApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtTokenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SchoolDbContext _dbContext;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="dbContext"></param>
        /// <param name="configuration"></param>
        public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, SchoolDbContext dbContext, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register-post")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide the data for all the required fields!");
            }

            User? userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest($"User {model.Email} already exists");
            }

            User user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                DateOfBirth = model.DateOfBirth,
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult? result = await _userManager.CreateAsync(user, model.Password);

            return result.Succeeded
                ? Ok("New User has been created successfully!")
                : BadRequest("The System failed to add a new user, please try again and contact the System Administrator if problem persists");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login-post")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide the data for all the required fields!");
            }

            User? userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null && await _userManager.CheckPasswordAsync(userExists, model.Password))
            {
                return Ok($"User {model.Email} Signed In!");
            }
            return Unauthorized();
        }
    }
}
