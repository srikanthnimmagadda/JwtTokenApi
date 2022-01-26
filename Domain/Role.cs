using Microsoft.AspNetCore.Identity;

namespace JwtTokenApi.Domain
{
    public class Role : IdentityRole<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? UpdatedByName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? UpdatedDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
