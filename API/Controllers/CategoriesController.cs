using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using API.Extensions;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
     [Authorize]
    public class CategoriesController : BaseApiController
    {
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly ICategoryService _categoryService;


        public CategoriesController(IGenericRepository<Category> categoryRepo, ICategoryService categoryService)
        {
            _categoryRepo = categoryRepo;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(
            [FromQuery] QueryParameters queryParameters
        )
        {
            var list = await _categoryService.ListAllAsync();

            if (queryParameters.HasQuery())
            {
                list = list
                .Where(t => t.CategoryName.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return Ok(list);
        }
        [HttpGet("paging")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories1(
            [FromQuery] QueryParameters queryParameters
        )
        {
            var list = await _categoryService.ListAllAsync();

            if (queryParameters.HasQuery())
            {
                list = list
                .Where(t => t.CategoryName.
                ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return Ok(list.OrderBy(t => t.CategoryName)
                          .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                          .Take(queryParameters.PageCount));
        }
        [HttpGet("pagingtup")]
        public async Task<ActionResult<Pagination1<Category>>> GetCategories2(
            [FromQuery] QueryParameters queryParameters
        )
        {
            var list = await _categoryService.ListAllAsync();

            if (queryParameters.HasQuery())
            {
                list = list
                .Where(t => t.CategoryName.
                ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            var listy = list.OrderBy(t => t.CategoryName)
                          .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                          .Take(queryParameters.PageCount);

            return Ok(new Pagination1<Category>
            (queryParameters.Page, queryParameters.PageCount, list.Count(), listy));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.FindCategoryByIdAsync(id);

            if (category == null) return NotFound(new ApiResponse(404));

            return Ok(category);

        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateCategoryAsync([FromBody] Category category)
        {
            await _categoryService.CreateCategoryAsync(category);

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest(new ApiResponse(400, "You lose:)"));
            }

            await _categoryService.UpdateCategoryAsync(category);

            return Ok(category);
        }
        [HttpPut("rez/{id}")]
        public async Task<ActionResult> UpdateCategory1(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest(new ApiResponse(400, "You lose:)"));
            }

            await _categoryService.UpdateCategoryAsync(category);

            return NoContent();
        }
        [HttpPut("edit")]
        public async Task<ActionResult<Category>> UpdateCategory1([FromBody] Category category)
        {
            await _categoryService.UpdateCategoryAsync(category);

            return Ok(category);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategory(category);

            return NoContent();
        }

    }
}











