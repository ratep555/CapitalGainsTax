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
using API.Helpers;
using API.Extensions;

namespace API.Controllers
{
    
    public class StocksController : BaseApiController
    {
        private readonly IGenericRepository<Stock> _stocksRepo;
        private readonly IStockService _stockService;
        private readonly IGenericRepository<Category> _categoriesRepo;
        private readonly IGenericRepository<Country> _countriesRepo;
        private readonly IMapper _mapper;

        public StocksController(IGenericRepository<Stock> stocksRepo,
        IStockService stockService,
        IGenericRepository<Category> categoriesRepo,
        IGenericRepository<Country> countriesRepo,
        IMapper mapper
        )
        {
            _stocksRepo = stocksRepo;
            _stockService = stockService;
            _categoriesRepo = categoriesRepo;
            _countriesRepo = countriesRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<StockToReturnDto>>> GetStocks(
            [FromQuery]StockSpecParams stockParams) 
        {
            var spec = new StocksWithCategoriesAndCountriesSpecification(stockParams);

            var countSpec = new StockWithFiltersForCountSpecification(stockParams);

            var totalItems = await _stocksRepo.CountAsync(countSpec);

            var stocks = await _stocksRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Stock>, IReadOnlyList<StockToReturnDto>>(stocks); 

            return Ok(new Pagination<StockToReturnDto>
            (stockParams.PageIndex, stockParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockToReturnDto1>> GetStock(int id) 
        {
            var email = User.RetrieveEmailFromPrincipal();

            var spec = new StocksWithCategoriesAndCountriesSpecification(id);
            
            var stock = await _stocksRepo.GetEntityWithSpec(spec);

            if(stock == null) return NotFound(new ApiResponse(404));

            var stocky = _mapper.Map<Stock, StockToReturnDto1>(stock);

            stocky.TotalQuantity = await _stockService.TotalQuantity(email, id);

            return stocky;

        }
        // ova je bez specifikacija
        [HttpGet("ajmoopet/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetStock1(int id) 
        {
            var email = User.RetrieveEmailFromPrincipal();
            
            var stock = await _stocksRepo.GetByIdAsync(id);

            if(stock == null) return NotFound(new ApiResponse(404));

            var stocky = _mapper.Map<Stock, StockToReturnDto1>(stock);

            stocky.TotalQuantity = await _stockService.TotalQuantity(email, id);

            return stocky.TotalQuantity;

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