using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    
    public class StocksController : BaseApiController
    {
        private readonly IGenericRepository<Stock> _stocksRepo;
        private readonly IGenericRepository<Category> _categoriesRepo;
        private readonly IGenericRepository<Country> _countriesRepo;
        private readonly IMapper _mapper;

        public StocksController(IGenericRepository<Stock> stocksRepo,
        IGenericRepository<Category> categoriesRepo,
        IGenericRepository<Country> countriesRepo,
        IMapper mapper
        )
        {
            _stocksRepo = stocksRepo;
            _categoriesRepo = categoriesRepo;
            _countriesRepo = countriesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<StockToReturnDto>>> GetStocks() 
        {
            var spec = new StocksWithCategoriesAndCountriesSpecification();

            var stocks = await _stocksRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Stock>, IReadOnlyList<StockToReturnDto>>(stocks)); 
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockToReturnDto>> GetStock(int id) 
        {
            var spec = new StocksWithCategoriesAndCountriesSpecification(id);
            
            var stock = await _stocksRepo.GetEntityWithSpec(spec);

            if(stock == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Stock, StockToReturnDto>(stock);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            return Ok(await _categoriesRepo.ListAllAsync());      
        }
        
        [HttpGet("countries")]
        public async Task<ActionResult<IReadOnlyList<Country>>> GetCountries()
        {
            return Ok(await _countriesRepo.ListAllAsync());     
        }
        
    }
}