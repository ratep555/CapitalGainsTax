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

    public TransactionsController(
    IGenericRepository<StockTransaction> transactionsRepo,
    IMapper mapper,
    ITransactionService transactionService)
    {
        _transactionsRepo = transactionsRepo;
        _mapper = mapper;
        _transactionService = transactionService;
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

         var list = await _transactionService.ShowTransactionsForSpecificUser1(queryParameters, email);

        return Ok(list);
    }
   /*  [HttpGet("pekidrekiski")]
    public async Task<ActionResult<TransactionsForUserVM>> GetTransactionsForSpecificUser3(
        [FromQuery]QueryParameters queryParameters)
    {
         var email = HttpContext.User.RetrieveEmailFromPrincipal();

         List<TransactionsForUserVM> list = await _transactionService
         .ShowTransactionsForSpecificUser2(queryParameters, email).ToListAsync();

        return Ok(list);
    } */
    [HttpPost]
    public async Task<ActionResult> CreateTransaction(StockTransaction transaction)
    {
        transaction.Date = DateTime.Now;
       // transaction.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        transaction.UserId = _transactionService.GetUserId();

        var stockTrans = await _transactionService.CreateTransaction(transaction);

        return Ok(stockTrans);
    }
    [HttpPost("kreat")]
    public async Task<ActionResult> CreateTransactionist(TransactionToCreateVM transactionVM)
    {
        var transaction = new StockTransaction 
        {
             Date = DateTime.Now,
             UserId = _transactionService.GetUserId(),
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
        var transaction = new StockTransaction 
        {
             Date = DateTime.Now,
             UserId = _transactionService.GetUserId(),
             StockId = id,
             Purchase = true,
             Quantity = transactionVM.Quantity,
             Price = transactionVM.Price,
             Resolved = transactionVM.Resolved
        };
       
         var transaction1 = await _transactionService.CreateTransaction(transaction);

        return Ok(transaction1);
    }
    [HttpPost("kreativissimo/{id}")]
    public async Task<ActionResult> CreateTransactionist2(int id, TransactionToCreateVM transactionVM)
    {
        var transaction = new StockTransaction 
        {
             Date = DateTime.Now,
             UserId = _transactionService.GetUserId(),
             StockId = id,
             Purchase = false,
             Quantity = transactionVM.Quantity,
             Price = transactionVM.Price,
             Resolved = transactionVM.Resolved
        };
       
         var transaction1 = await _transactionService.CreateTransaction1(transaction, id, _transactionService.GetUserId());

        return Ok(transaction1);
    }
}
}








