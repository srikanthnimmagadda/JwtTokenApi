using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JwtTokenApi.Domain
{
    public class UserRole : IdentityUserRole<int>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int UserRoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override int UserId { get => base.UserId; set => base.UserId = value; }

        /// <summary>
        /// 
        /// </summary>
        public override int RoleId { get => base.RoleId; set => base.RoleId = value; }
    }
}
