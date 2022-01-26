using System.ComponentModel.DataAnnotations;

namespace JwtTokenApi.ViewModels
{
    public class TokenRequestViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string RefreshToken { get; set; }
    }
}
