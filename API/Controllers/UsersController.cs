using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Errors;
using API.Extensions;
using API.Helpers;
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
        // ovo šljaka!
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination1<UserToReturnDto>>> GetUsers(
            [FromQuery] QueryParameters queryParameters
        )
        {
            var email = User.RetrieveEmailFromPrincipal();

            var list = await _userService.ListAllUsersAsync(email);

             if (queryParameters.HasQuery())
            {
                list = list
                .Where(t => t.Email.
                ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            var listy = list.OrderBy(t => t.DisplayName)
                          .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                          .Take(queryParameters.PageCount);

            return Ok(new Pagination1<UserToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, list.Count(), listy));
        }
      
        //šljaka!
        [HttpPut("luckily/{id}")]
        public async Task<ActionResult> UnLockUser(string id)
        {
            var user = await _userService.FindUserByIdAsync(id);

            if (user == null)
            {
                  return NotFound(new ApiResponse(404));
            }

           await _userService.UnLockUser(id);

           return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> LockUser1(string id)
        {
            var user = await _userService.FindUserByIdAsync(id);

            if (user == null)
            {
                  return NotFound(new ApiResponse(404));
            }

           await _userService.LockUser(id);

           return Ok();
        }

    }
}


