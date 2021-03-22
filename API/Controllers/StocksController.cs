using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository _repo;

        public StocksController(IStockRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Stock>>> GetStocks() 
        {
            var stocks = await _repo.GetStocksAsync();

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(int id) 
        {
            return await _repo.GetStockByIdAsync(id);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            return Ok(await _repo.GetCategoriesAsync());      
        }
        
        [HttpGet("countries")]
        public async Task<ActionResult<IReadOnlyList<Country>>> GetCountries()
        {
            return Ok(await _repo.GetCountriesAsync());     
        }
        
    }
}