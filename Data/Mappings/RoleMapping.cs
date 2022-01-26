using JwtTokenApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtTokenApi.Data.Mappings
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable($"{nameof(Role)}").Property(p => p.Id).HasColumnName($"{nameof(Role)}Id");
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Description).IsRequired().HasMaxLength(250);
            builder.Property(e => e.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(e => e.UpdatedBy).HasMaxLength(100);
            builder.Property(e => e.UpdatedByName).HasMaxLength(150);
            builder.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(0);
        }
    }
}
