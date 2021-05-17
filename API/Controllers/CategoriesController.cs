using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories(
            [FromQuery]QueryParameters queryParameters
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
            [FromQuery]QueryParameters queryParameters
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
            [FromQuery]QueryParameters queryParameters
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

        [HttpPost("create")]
        public async Task<ActionResult> CreateCategoryAsync([FromBody] Category category)
        {
            await _categoryService.CreateCategoryAsync(category);

            return Ok(category);
        }
    }
}