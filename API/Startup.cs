using System.IdentityModel.Tokens.Jwt;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using AutoMapper;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            // maknuo si ovo iako ti prolazi za userid u jwt
           // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            _config = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            
            services.AddControllers();

            services.AddDbContext<StoreContext>(options =>
               options.UseSqlServer(
                   _config.GetConnectionString("DefaultConnection")));

           
            services.AddApplicationServices();
            services.AddIdentityServices(_config);

            // možeš u startup ovo staviti, pogledaj si code maze, ne moraš
            // stavljati kod password dodatna pravila!
           /*  services.AddIdentity<AppUser, IdentityRole>(opt => 
              {
               opt.Password.RequiredLength = 7;
               opt.Password.RequireDigit = false;
               opt.User.RequireUniqueEmail = true;
              }); */

            services.AddSwaggerDocumentation();
           // services.AddHttpContextAccessor();

            
            //dodao si novo httpclient
            services.AddHttpClient();


            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
