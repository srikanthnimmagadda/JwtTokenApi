namespace JwtTokenApi.ViewModels
{
    public class AuthResultViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RefresToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}
