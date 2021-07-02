using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.ViewModels;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserToReturnDto>> ListAllUsersAsync(string email);
        Task<IQueryable<UserDto1>> ListAllUsersAsync1(string email);
        Task<IQueryable<UserDto1>> ListAllUsersAsync2(string userId);
        Task<IQueryable<UserDto1>> ListAllUsersAsync3(string userId, string email);
        Task<AppUser> FindUserByIdAsync(string userId);

        Task LockUser(string id);
        Task UnLockUser(string id);

        Task<bool> CheckIfRoleExists();


        Task<string> RoleName(string userId);
        string RoleName1(string userId);


    }
}