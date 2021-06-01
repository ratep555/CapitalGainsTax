using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using API.Dtos;
using API.Errors;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.ViewModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [Authorize]
    public class TransactionsController : BaseApiController
    {
    private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;
        private readonly IGenericRepository<StockTransaction> _transactionsRepo;
        private readonly IGenericRepository<Stock> _stocksRepo;

    public TransactionsController(
    IGenericRepository<StockTransaction> transactionsRepo,
    IMapper mapper,
    ITransactionService transactionService,
    IGenericRepository<Stock> stocksRepo)
    {
        _transactionsRepo = transactionsRepo;
        _mapper = mapper;
        _transactionService = transactionService;
        _stocksRepo = stocksRepo;
        }
    [HttpGet]
    public async Task<ActionResult<Pagination<TransactionToReturnDto>>> GetTransactionsAsync(
        [FromQuery]TransactionSpecParams transactionParams)
    {
        var spec = new TransactionsWithStocksSpecification(transactionParams);

        var countSpec = new TransactionWithFiltersForCountSpecification(transactionParams);

        var totalItems = await _transactionsRepo.CountAsync(countSpec);
       
        var transactions = await _transactionsRepo.ListAsync(spec);

        var data = _mapper
        .Map<IReadOnlyList<StockTransaction>, IReadOnlyList<TransactionToReturnDto>>(transactions);

        return Ok(new Pagination<TransactionToReturnDto>(transactionParams.PageIndex, 
            transactionParams.PageSize, totalItems, data));   
    }
    /* [HttpGet("getty")]
    public async Task<ActionResult<IReadOnlyList<TransactionToReturnDto>>> GetTransactionsForSpecificUserAsync()
    {
        var userId = HttpContext.User.RetrieveIdFromPrincipal();

        var transactions = await _transactionService.GetTransactionsForUserAsync(userId);

        return Ok( _mapper.Map<IReadOnlyList<StockTransaction>, IReadOnlyList<TransactionToReturnDto>>(transactions));

    } */
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionToReturnDto>> GetTransactionAsync(int id)
    {
        var spec = new TransactionsWithStocksSpecification(id);

        var transaction = await _transactionsRepo.GetEntityWithSpec(spec);

        if(transaction == null)
        return NotFound(new ApiResponse(404));

        return _mapper.Map<StockTransaction, TransactionToReturnDto>(transaction);

    }
   /*  [HttpGet("gogy")]
    public async Task<ActionResult<IReadOnlyList<StockTransaction>>> GetAllTransactions()
    {
        var list = await _transactionsRepo.ListAllAsync();

        return Ok(list);
    } */

  /*   [HttpPost]
    public async Task<ActionResult<StockTransaction>> CreateTransaction(StockTransaction stockTransaction)
    {
        var userId = HttpContext.User.RetrieveIdFromPrincipal();
        
        var transaction = await _transactionService.CreateTransactionAsync(userId);

        if(transaction == null) 
        return BadRequest(new ApiResponse(400, "Problem creating order"));

        return Ok(transaction);
    } */

    [HttpGet("peki")]
    public async Task<ActionResult<IEnumerable<TransactionsForUserVM>>> GetTransactionsForSpecificUser()
    {
         var email = HttpContext.User.RetrieveEmailFromPrincipal();

         var list = await _transactionService.ShowTransactionsForSpecificUser(email);

        return Ok(list);
    }
    [HttpGet("pekismeki")]
    public async Task<ActionResult<IQueryable<TransactionsForUserVM>>> GetTransactionsForSpecificUser1(
        [FromQuery]QueryParameters queryParameters)
    {
         var email = HttpContext.User.RetrieveEmailFromPrincipal();

         var list = await _transactionService.ShowTransactionsForSpecificUser1(queryParameters, email);

        return Ok(list);
    }
    [HttpGet("pekidreki")]
    public async Task<ActionResult<IQueryable<TransactionsForUserVM>>> GetTransactionsForSpecificUser2(
        [FromQuery]QueryParameters queryParameters)
    {
         var email = HttpContext.User.RetrieveEmailFromPrincipal();

         var list = await _transactionService.ShowTransactionsForSpecificUser3(queryParameters, email);

        if (queryParameters.HasQuery())
        {
            list = list
                .Where(t => t.Stock.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()
                ));
        } 

        return Ok(list);
    }
   /*  [HttpGet("pekidreki")]
    public async Task<ActionResult<IQueryable<TransactionsForUserVM>>> GetTransactionsForSpecificUser2(
        [FromQuery]QueryParameters queryParameters)
    {
         var email = HttpContext.User.RetrieveEmailFromPrincipal();

         var list = await _transactionService.ShowTransactionsForSpecificUser1(queryParameters, email);

        return Ok(list);
    } */

    [HttpPost]
    public async Task<ActionResult> CreateTransaction(StockTransaction transaction)
    {
        transaction.Date = DateTime.Now;
       // transaction.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        transaction.UserId = await _transactionService.GetUserId();

        var stockTrans = await _transactionService.CreateTransaction(transaction);

        return Ok(stockTrans);
    }
    [HttpPost("kreat")]
    public async Task<ActionResult> CreateTransactionist(TransactionToCreateVM transactionVM)
    {
        var transaction = new StockTransaction 
        {
             Date = DateTime.Now,
             UserId = await _transactionService.GetUserId(),
             StockId = transactionVM.StockId,
             Purchase = transactionVM.Purchase,
             Quantity = transactionVM.Quantity,
             Price = transactionVM.Price,
             Resolved = transactionVM.Resolved

        };
       
         var transaction1 = await _transactionService.CreateTransaction(transaction);

        return Ok(transaction1);
    }
    [HttpPost("kreativo/{id}")]
    public async Task<ActionResult> CreateTransactionist1(int id, TransactionToCreateVM transactionVM)
    {     
        transactionVM.Email = User.RetrieveEmailFromPrincipal();

        var transaction = new StockTransaction                                                                                                    
        {
             Id = transactionVM.Id,
             Date = DateTime.Now,
             StockId = id,
             Purchase = true,
             Quantity = transactionVM.Quantity,
             Price = transactionVM.Price,
             Resolved = transactionVM.Resolved,
             Email = transactionVM.Email
        };
       
        var transaction1 = await _transactionService.CreateTransaction(transaction);

        return Ok(transaction1);
    }
    [HttpPost("kreativissimo/{id}")]
    public async Task<ActionResult> CreateTransactionist2(int id, TransactionToCreateVM transactionVM)
    {
        transactionVM.Email = User.RetrieveEmailFromPrincipal();

       // var userId = await _transactionService.GetUserId();

        if(await _transactionService.TotalQuantity(transactionVM.Email, id) < transactionVM.Quantity)
        {
              return new BadRequestObjectResult
              (new ApiValidationErrorResponse{Errors = new []{"You are selling more than you have!"}});
        }

        var transaction = new StockTransaction 
        {
             Id = transactionVM.Id,
             Date = DateTime.Now,
             StockId = id,
             Purchase = false,
             Quantity = transactionVM.Quantity,
             Price = transactionVM.Price,
             Resolved = transactionVM.Resolved,
             Email = transactionVM.Email
        };
       
        var transaction1 = await _transactionService
        .CreateTransaction1(transaction, id, User.RetrieveEmailFromPrincipal());

        return Ok(transaction1);
    }    
    [HttpGet("exceed/{id1}/{id2}")]
    public async Task<ActionResult<bool>> CheckTotalQuantity(int transactionId, 
    int stockId, 
    [FromQuery]string quantity)
    {
        var email = User.RetrieveEmailFromPrincipal();

        var transaction = await _transactionsRepo.GetByIdAsync(transactionId);

        if (await _transactionService.TotalQuantity(email, stockId) < transaction.Quantity)
        {
            return true;
        }
            return false;
    }
    [HttpGet("exceedy/{id}/{id1}")]
    public async Task<ActionResult<bool>> CheckTotalQuantity1( 
    int stockId, int quantity, [FromQuery] string email)
    {
        email = User.RetrieveEmailFromPrincipal();
        var stock = _transactionService.FindStockById(stockId);

        var transaction = await _transactionService.GetTransactionByEmailAndId(email, stockId, quantity);
        /* transaction.Quantity = quantity;
        transaction.Email = email;
        transaction.StockId = stockId; */

        if (await _transactionService
        .TotalQuantity(email, stock.Id) < quantity)
        {
            return true;
        }
            return false;
    }
    [HttpGet("profit")]
    public async Task<ActionResult<decimal>> CheckTotalProfit()
    {
        var email = User.RetrieveEmailFromPrincipal();

        var totalProfit = await _transactionService.TotalNetProfit(email);

        return totalProfit;

    }
    [HttpGet("profitwow")]
    public async Task<ActionResult<TaxLiabilityVM>> CheckTotalProfit1()
    {
        var email = User.RetrieveEmailFromPrincipal();

        var taxLiability = await _transactionService.ReturnTaxLiability(email);

        return Ok(taxLiability);

    }
  }
}








