using JwtTokenApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtTokenApi.Data.Mappings
{
    public class UserRoleMapping : IEntityTypeConfiguration<UserRole>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable($"{nameof(UserRole)}");
            builder.Property(e => e.UserRoleId).UseIdentityColumn();
        }
    }
}
