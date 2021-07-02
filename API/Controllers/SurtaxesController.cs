using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SurtaxesController : BaseApiController
    {
        private readonly ISurtaxService _surtaxService;
        public SurtaxesController(ISurtaxService surtaxService)
        {
            _surtaxService = surtaxService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination1<Surtax>>> GetSurtaxes(
            [FromQuery] QueryParameters queryParameters
        )
        {
            var list = await _surtaxService.ListAllAsync();

            if (queryParameters.HasQuery())
            {
                list = list
                .Where(t => t.Residence.
                ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            var listy = list.OrderBy(t => t.Residence)
                          .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                          .Take(queryParameters.PageCount);

            return Ok(new Pagination1<Surtax>
            (queryParameters.Page, queryParameters.PageCount, list.Count(), listy));
        }
        [HttpGet("gethim")]
        public async Task<ActionResult<Surtax>> GetCoreSurtaxes()
        {
            var list = await _surtaxService.ListAllAsync1();

            return Ok(list);
        }
    }
}