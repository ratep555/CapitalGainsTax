using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        public UsersController(IUserService userService, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _userService = userService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto1>>> GetUsers()
        {
            var list = await _userService.ListAllUsersAsync();

            return Ok(list);
        }
        [Authorize]
        [HttpGet("ja")]
        public async Task<ActionResult> GetUsers1()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var list = await _userService.ListAllUsersAsync1(email);

            return Ok(list);
        }
        [Authorize]
        [HttpGet("jako")]
        public async Task<ActionResult> GetUsers2()
        {
            var email = User.RetrieveEmailFromPrincipal();
            var user = await _userManager.FindByEmailAsync(email);
            var userId = user.Id;
            
            var list = await _userService.ListAllUsersAsync2(userId);

            return Ok(list);
        }
    }
}


