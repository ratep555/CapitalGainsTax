using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.ViewModels;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly StoreContext _context;
        public UserService(StoreContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<UserDto1>> ListAllUsersAsync()
        {
            var list = await(from u in _context.Users
                             join ur in _context.UserRoles
                             on u.Id equals ur.UserId
                             join r in _context.Roles
                             on ur.RoleId equals r.Id
                             select new UserDto1 
                             {
                                 DisplayName = u.UserName,
                                 Email = u.Email,
                                 RoleName = r.Name,
                                 UserId = u.Id
                             }).ToListAsync(); 

            return await Task.FromResult(list);   
        
        } 
        public async Task<IQueryable<UserDto1>> ListAllUsersAsync1(string email)
        {
            IQueryable<UserDto1> list = (from u in _context.Users
                             .Where(u => u.Email != email)                             
                             join ur in _context.UserRoles
                             on u.Id equals ur.UserId
                             join r in _context.Roles
                             on ur.RoleId equals r.Id
                             select new UserDto1 
                             {
                                 DisplayName = u.UserName,
                                 Email = u.Email,
                                 RoleName = r.Name,
                                 UserId = u.Id
                             }).AsQueryable(); 

            return await Task.FromResult(list);   
        
        } 
        public async Task<IQueryable<UserDto1>> ListAllUsersAsync2(string userId)
        {
            IQueryable<UserDto1> list = (from u in _context.Users
                             .Where(u => u.Id != userId)                             
                             join ur in _context.UserRoles
                             on u.Id equals ur.UserId
                             join r in _context.Roles
                             on ur.RoleId equals r.Id
                             select new UserDto1 
                             {
                                 DisplayName = u.UserName,
                                 Email = u.Email,
                                 RoleName = r.Name,
                                 UserId = u.Id
                             }).AsQueryable(); 

            return await Task.FromResult(list);   
        
        } 

        public async Task<string> RoleName(string userId)
        {
            var roleName = ( from r in _context.Roles
                               join ur in _context.UserRoles
                               on r.Id equals ur.RoleId
                               join u in _context.Users.Where(u => u.Id == userId)
                               on ur.UserId equals u.Id
                               select r.Name
                               ).Single();
            
            return await Task.FromResult(roleName);
        }
        public string RoleName1(string userId)
        {
            var roleName = ( from r in _context.Roles
                               join ur in _context.UserRoles
                               on r.Id equals ur.RoleId
                               join u in _context.Users.Where(u => u.Id == userId)
                               on ur.UserId equals u.Id
                               select r.Name
                               ).Single();
            
            return roleName;
        }
    }
}







