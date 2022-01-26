using System.ComponentModel.DataAnnotations;

namespace JwtTokenApi.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
