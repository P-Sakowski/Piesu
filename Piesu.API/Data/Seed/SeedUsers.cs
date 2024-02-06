using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Piesu.API.Data.Seed
{
    public class SeedUsers
    {
        public static void Seed(UserManager<ApplicationUser> userManager)
        {
            var foo = Task.Run(async () => await userManager.FindByNameAsync("Administrator")).Result;
            if (foo == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "Administrator",
                    Email = "admin@piesu.pl"
                };

                Task.Run(async () => await userManager.CreateAsync(user, "@Admin123"));
                Task.Run(async () => await userManager.AddToRoleAsync(user, "Administrator"));
            }
        }
    }
}
