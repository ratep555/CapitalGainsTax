using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StocksAdminController : BaseApiController
    {
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;
        public StocksAdminController(IStockService stockService, IMapper mapper)
        {
            _mapper = mapper;
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination1<StockToReturnDto>>> GetStocks(
            [FromQuery] QueryParameters queryParameters)
        {
            var list = await _stockService.ListAllStocksAsync();

            var data = _mapper.Map<IEnumerable<Stock>, IEnumerable<StockToReturnDto>>(list); 

            if (queryParameters.HasQuery())
            {
                data = data
                .Where(t => t.Symbol.
                ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            var listy = data.OrderBy(t => t.Symbol)
                          .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                          .Take(queryParameters.PageCount);

            return Ok(new Pagination1<StockToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, list.Count(), listy));
        }
        // ovo koristiš, malo kraći kod od ovoga gore
        [HttpGet("novi")]
        public async Task<ActionResult<Pagination1<StockToReturnDto>>> GetStocks1(
            [FromQuery] QueryParameters queryParameters)
        {
            var list = await _stockService.ListAllStocksAsync1(queryParameters);

            var data = _mapper.Map<IEnumerable<Stock>, IEnumerable<StockToReturnDto>>(list); 

            data = data.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                       .Take(queryParameters.PageCount);
            

            return Ok(new Pagination1<StockToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, list.Count(), data));
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var list = await _stockService.ListAllCategoriesAsync();

            return Ok(list);
        }

        [HttpGet("countries")]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var list = await _stockService.ListAllCountriesAsync();

            return Ok(list);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateCountryAsync([FromBody] Stock stock)
        {
           // var stock = _mapper.Map<StockToReturnDto, Stock>(stockDTO);
            
            await _stockService.CreateStockAsync(stock); //1(stock, stockDTO.Category, stockDTO.Country);

            return Ok(stock);
        }

        // ovo šljaka
        [HttpPost("createtu")]
        public async Task<ActionResult> CreateStockAsync([FromBody] StockToReturnDto stockDTO)
        {
            Stock stock = new Stock 
            {
                Symbol = stockDTO.Symbol,
                CompanyName = stockDTO.CompanyName,
                CurrentPrice = stockDTO.CurrentPrice,
                CategoryId = 1,
                CountryId = 1
            }; 

            await _stockService.CreateStockAsync(stock);

            var stockDto = _mapper.Map<Stock, StockToReturnDto>(stock);

            return Ok(stockDto);
        }
      
        [HttpGet("getquantity/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> GetTotalQuantity(int stockId)
        {
            var email = User.RetrieveEmailFromPrincipal();

            var stock = _stockService.FindStockById(stockId);

            var quantity = await _stockService.TotalQuantity(email, stock.Id);

            return Ok(quantity);
        }
    
        //ovo isto šljaka, koristiš je u angularu za create, najbolja jer koristiš najmanje koda
        //ovako radi neil:)
        [HttpPost("pikilili")]
        public async Task<ActionResult<StockToCreateDto>> CreateStockAsyncPip([FromBody] StockToCreateDto stockDTO)
        {
            var stock = _mapper.Map<StockToCreateDto, Stock>(stockDTO);

            await _stockService.CreateStockAsync(stock);

            return _mapper.Map<Stock, StockToCreateDto>(stock);

          //  return Ok( stockDto);
        }
        // šljaka, standardna verzija
        [HttpPost("pikililil")]
        public async Task<ActionResult> CreateStockAsyncPip1([FromBody] Stock stock)
        {
            await _stockService.CreateStockAsync(stock);

            return Ok(stock);
        }  

        [HttpPut("{id}")]
        public async Task<ActionResult<StockToEditDto>> PutBankAccount(int id, [FromBody] StockToEditDto stockToEditDto)
        {
            var stock = _mapper.Map<StockToEditDto, Stock>(stockToEditDto);

            if (id != stock.Id)
            {
                   return BadRequest(new ApiResponse(400));
            }

            await _stockService.UpdateStockAsync(stock);

            //indijac u svom predlošku vraća nocontent
            return _mapper.Map<Stock, StockToEditDto>(stock);
        }
        [HttpGet("getter")]
        public async Task<ActionResult<IEnumerable<Stock>>> GetBankAccounts()
        {
            return await _stockService.GetStocksAsync();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockToCreateDto>> GetStock(int id) 
        {
            
            var stock = await _stockService.GetByIdAsync(id);

            if(stock == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Stock,StockToCreateDto>(stock);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _stockService.GetByIdAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            await _stockService.DeleteStockAsync(stock);

            return NoContent();
        }
    }
}










