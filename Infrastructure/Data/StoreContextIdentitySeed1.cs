using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class StoreContextIdentitySeed1
    {
         public static async Task SeedUsersAsync1(
         UserManager<AppUser> userManager, 
         RoleManager<IdentityRole> roleManager)
        {
            string role = "Admin";

            var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }

            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    SurtaxId = 1
                 
                };

                IdentityResult identityResult = userManager.CreateAsync(user, "Pa$$w0rd").Result;

                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }   
                         
            }
        }
    }
    }
