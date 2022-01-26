using JwtTokenApi.Data.Mappings;
using JwtTokenApi.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JwtTokenApi.Data
{
    public class SchoolDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserMapping());
            builder.ApplyConfiguration(new RoleMapping());
            builder.ApplyConfiguration(new UserRoleMapping());

            builder.Entity<UserRole>(entity =>
            {
                entity.HasKey(x => x.UserRoleId);
            });

            builder.Entity<IdentityUserLogin<int>>(i =>
            {
                i.ToTable("UserLogin");
                i.HasIndex(x => new
                {
                    x.ProviderKey,
                    x.LoginProvider
                });
            });

            builder.Entity<IdentityRoleClaim<int>>(i =>
            {
                i.ToTable("RoleClaim");
                i.HasKey(x => x.Id);
            });

            builder.Entity<IdentityUserClaim<int>>(i =>
            {
                i.ToTable("UserClaim");
                i.HasKey(x => x.Id);
            });

            builder.Entity<IdentityUserToken<int>>(i =>
            {
                i.ToTable("UserToken");
                i.HasKey(x => x.UserId);
            });
        }
    }
}
