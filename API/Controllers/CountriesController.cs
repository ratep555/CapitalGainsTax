using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class CountriesController : BaseApiController
    {
        private readonly ICountryService _countryService;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination1<Country>>> GetCountries(
            [FromQuery] QueryParameters queryParameters
        )
        {
            var list = await _countryService.ListAllAsync();

            if (queryParameters.HasQuery())
            {
                list = list
                .Where(t => t.CountryName.
                ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            var listy = list.OrderBy(t => t.CountryName)
                          .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                          .Take(queryParameters.PageCount);

            return Ok(new Pagination1<Country>
            (queryParameters.Page, queryParameters.PageCount, list.Count(), listy));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> GetCategory(int id)
        {
            var country = await _countryService.FindCountryByIdAsync(id);

            if (country == null) return NotFound(new ApiResponse(404));

            return Ok(country);

        }
        
        [HttpPost("create")]
        public async Task<ActionResult> CreateCountryAsync([FromBody] Country country)
        {
            await _countryService.CreateCountryAsync(country);

            return Ok(country);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> UpdateCountry(int id, [FromBody] Country country)
        {
            if (id != country.Id)
            {
                return BadRequest(new ApiResponse(400, "You lose:)"));
            }

            await _countryService.UpdateCountryAsync(country);

            return Ok(country);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countryService.FindCountryByIdAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            await _countryService.DeleteCountry(country);

            return NoContent();
        }
    }
}










