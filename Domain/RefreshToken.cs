using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtTokenApi.Domain
{
    public class RefreshToken
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string JwtId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRevoked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TokenAddedOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime TokenExpiredOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public User User { get; set; }
    }
}
