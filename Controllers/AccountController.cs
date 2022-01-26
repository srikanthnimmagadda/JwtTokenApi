using JwtTokenApi.Data;
using JwtTokenApi.Domain;
using JwtTokenApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly TokenValidationParameters _tokenValidationParameters;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="dbContext"></param>
        /// <param name="configuration"></param>
        public AccountController(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SchoolDbContext dbContext,
            IConfiguration configuration,
            TokenValidationParameters tokenValidationParameters)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
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
                var jwtToken = await GenerateJwtTokenAsync(userExists, null);
                return Ok(jwtToken);
            }
            return Unauthorized();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide the data for all the required fields!");
            }
            var result = await VerifyAndGenerateTokenAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<AuthResultViewModel> VerifyAndGenerateTokenAsync(TokenRequestViewModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var refreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == model.RefreshToken);
            var user = await _userManager.FindByIdAsync(refreshToken?.UserId.ToString());
            try
            {
                var tokenCheckResult = jwtTokenHandler.ValidateToken(model.Token, _tokenValidationParameters, out var validateToken);
                return await GenerateJwtTokenAsync(user, refreshToken);
            }
            catch (SecurityTokenExpiredException)
            {
                if (refreshToken?.TokenExpireOn >= DateTime.UtcNow)
                {
                    return await GenerateJwtTokenAsync(user, refreshToken);
                }
                else
                {
                    return await GenerateJwtTokenAsync(user, null);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rToken"></param>
        /// <returns></returns>
        private async Task<AuthResultViewModel> GenerateJwtTokenAsync(User user, RefreshToken? rToken)
        {
            List<Claim>? authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            SymmetricSecurityKey? authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            JwtSecurityToken? token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256));

            string? jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (rToken != null)
            {
                AuthResultViewModel? rTokenResponse = new AuthResultViewModel
                {
                    Token = jwtToken,
                    RefresToken = rToken.Token,
                    ExpiresAt = token.ValidTo
                };

                return rTokenResponse;
            }

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                TokenAddedOn = DateTime.UtcNow,
                TokenExpireOn = DateTime.UtcNow.AddMonths(6),
                Token = $"{Guid.NewGuid()}-{Guid.NewGuid()}"
            };

            await _dbContext.RefreshTokens.AddAsync(refreshToken);
            await _dbContext.SaveChangesAsync();

            AuthResultViewModel? response = new AuthResultViewModel
            {
                Token = jwtToken,
                RefresToken = refreshToken.Token,
                ExpiresAt = token.ValidTo
            };

            return response;
        }
    }
}
