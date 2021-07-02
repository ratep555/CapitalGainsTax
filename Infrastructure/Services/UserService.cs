using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
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
        public async Task<IEnumerable<UserToReturnDto>> ListAllUsersAsync(string email)
        {
            var list = await(from u in _context.Users.Where(u => u.Email != email)
                             join a in _context.AppUsers on u.Id equals a.Id                            
                             select new UserToReturnDto 
                             {
                                 DisplayName = a.DisplayName,
                                 Email = u.Email,
                                 UserId = u.Id,
                                 LockoutEnd = u.LockoutEnd
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
         public async Task<IQueryable<UserDto1>> ListAllUsersAsync3(string userId, string email)
        {
            IQueryable<UserDto1> list = (from u in _context.Users
                             .Where(u => u.Id != userId)                             
                             select new UserDto1 
                             {
                                 DisplayName = u.UserName,
                                 Email = email,
                                 UserId = u.Id
                             }).AsQueryable(); 

            return await Task.FromResult(list);   
        
        } 

        public async Task<string> RoleName(string userId)
        {
            var roleName = await( from r in _context.Roles
                               join ur in _context.UserRoles
                               on r.Id equals ur.RoleId
                               join u in _context.Users.Where(u => u.Id == userId)
                               on ur.UserId equals u.Id
                               select r.Name
                               ).FirstOrDefaultAsync();
            
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
                               ).FirstOrDefault();
            
            return roleName;
        }
        public async Task<bool> CheckIfRoleExists()
        {
            return await _context.Roles.AnyAsync();
        }

        public async Task LockUser(string id)
        {
            var userFromDb = await _context.AppUsers.Where(u => u.Id == id).FirstOrDefaultAsync();

            userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);

            _context.SaveChanges();
        }
        public async Task UnLockUser(string id)
        {
            var userFromDb = await _context.AppUsers.Where(u => u.Id == id).FirstOrDefaultAsync();

            userFromDb.LockoutEnd = null;

            _context.SaveChanges();
        }
        public async Task<AppUser> FindUserByIdAsync(string userId)
        {
            return await _context.AppUsers.Where(a => a.Id == userId).FirstOrDefaultAsync();
        }
    }
}







