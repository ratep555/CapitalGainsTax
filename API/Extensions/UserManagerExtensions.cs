using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser> input, 
        ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }    
        public static string RetrieveUserIdFromPrincipal(this UserManager<AppUser> input,
        ClaimsPrincipal user) 
        {
           return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public static int UserId(this UserManager<AppUser> input, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var usery = input.FindByEmailAsync(email);
            var userId = usery.Id;

            return userId;
        }
        /* public static AppUser CheckIfUserMatches(this UserManager<AppUser> input, string email)
        {
            var user = input.FindByEmailAsync(email);

            return user;
        } */
    }
}







