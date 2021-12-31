using System.Security.Claims;
using System.Text;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
        {
            var builder = services.AddIdentityCore<AppUser>();        

            builder = new IdentityBuilder(builder.UserType, builder.Services);
            
            //ovu liniju koda dolje si dodao naknadno zbog rola, štima iako nije po ps-u
            //pogledaj kod felipea kako je to napravljeno za role, i u paycompute identično!
            builder.AddRoles<IdentityRole>();
            builder.AddEntityFrameworkStores<StoreContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();

           /*  services.Configure<IdentityOptions>(options => 
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier); */

          // ovo ti je alternativa iz code maze, ne šljaka baš!
          /* services.AddIdentity<AppUser, IdentityRole>(opt => 
          {
           opt.Password.RequiredLength = 7;
           opt.Password.RequireDigit = false;
           opt.User.RequireUniqueEmail = true;
          })
          .AddEntityFrameworkStores<StoreContext>(); */
          
           services.AddAuthentication();

           services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                    };
                }); 

            return services;
        }
    }
}