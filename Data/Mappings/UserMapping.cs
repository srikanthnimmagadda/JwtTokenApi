using JwtTokenApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtTokenApi.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable($"{nameof(User)}").Property(p => p.Id).HasColumnName($"{nameof(User)}Id");
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.UserName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.PhoneNumber).HasMaxLength(50);
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.MiddleName).HasMaxLength(50);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            builder.Property(e => e.DateOfBirth);
            builder.Property(e => e.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.UpdatedByName).HasMaxLength(150);
            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(0);
        }
    }
}
