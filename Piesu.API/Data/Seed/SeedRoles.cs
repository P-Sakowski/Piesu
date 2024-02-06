using Microsoft.AspNetCore.Identity;

namespace Piesu.API.Data.Seed
{
    public class SeedRoles
    {
        public static void Seed(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };
                roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync("Moderator").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Moderator"
                };
                roleManager.CreateAsync(role);
            }
        }
    }
}
