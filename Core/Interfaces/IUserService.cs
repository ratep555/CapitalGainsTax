using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ViewModels;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto1>> ListAllUsersAsync();
        Task<IQueryable<UserDto1>> ListAllUsersAsync1(string email);
        Task<IQueryable<UserDto1>> ListAllUsersAsync2(string userId);
        Task<string> RoleName(string userId);
        string RoleName1(string userId);


    }
}