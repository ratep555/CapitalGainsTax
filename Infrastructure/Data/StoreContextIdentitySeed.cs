using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class StoreContextIdentitySeed
    {       
    public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com"
                 
                };
                //here we are passing password
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}