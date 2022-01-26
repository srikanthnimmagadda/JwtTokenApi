using JwtTokenApi.Domain;
using Microsoft.AspNetCore.Identity;

namespace JwtTokenApi.Data
{
    public class DbInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appBuilder"></param>
        /// <returns></returns>
        public static async Task SeedRolesToDataase(IApplicationBuilder appBuilder)
        {
            using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
                if (!await roleManager.RoleExistsAsync(Helpers.UserRole.Manager))
                {
                    await roleManager.CreateAsync(new Role { Name = Helpers.UserRole.Manager, Description = "Role for a Manager" });
                }

                if (!await roleManager.RoleExistsAsync(Helpers.UserRole.Student))
                {
                    await roleManager.CreateAsync(new Role { Name = Helpers.UserRole.Student, Description = "Role for a Student" });
                }
            }
        }
    }
}
