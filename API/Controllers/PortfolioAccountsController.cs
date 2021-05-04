using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Extensions;
using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class PortfolioAccountsController : BaseApiController
    {
        private readonly IStockRepository __repo;
       // private readonly UserManager<AppUser> _userManager;
        public PortfolioAccountsController(IStockRepository _repo
       // UserManager<AppUser> userManager
        )
        {
            __repo = _repo;
           // _userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientPortfolioViewModel>>> GetClientPortfolio(
            [FromQuery]QueryParameters queryParameters
        )
        {
            // var userId = HttpContext.User.RetrieveIdFromPrincipal();
           // var userId = User.GetLoggedInUserId<string>(); // Specify the type of your UserId;

           // var user = await _userManager.FindByEmailFromClaimsPrinciple(User);

            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var list = await __repo.ShowClientPortfolio(queryParameters, email);

            return Ok(list);
        }
        [HttpGet("zeko")]
        public async Task<ActionResult<IQueryable<ClientPortfolioViewModel>>> GetClientPortfolio1()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var list = await __repo.ShowClientPortfolio1(email);

            return Ok(list.GroupBy(d => d.Symbol).Select(d => d.FirstOrDefault()));
        }
        [HttpGet("yes")]
        public async Task<ActionResult<IEnumerable<ClientPortfolioViewModel>>> GetClientPortfolio2(
            [FromQuery]QueryParameters queryParameters
        )
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var list = await __repo.ShowClientPortfolio2(queryParameters, email, __repo.GetUserId());

             if (queryParameters.HasQuery())
            {
            list = list
                    .Where(t => t.Symbol.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return Ok(list);
        }
    }
}













