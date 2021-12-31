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
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Controllers
{
    [Authorize]
    public class TransactionsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;
        private readonly IGenericRepository<StockTransaction> _transactionsRepo;
        private readonly IGenericRepository<Stock> _stocksRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockService _stockService;

        public TransactionsController(
    IGenericRepository<StockTransaction> transactionsRepo,
    IMapper mapper,
    ITransactionService transactionService,
    IGenericRepository<Stock> stocksRepo,
    UserManager<AppUser> userManager,
    IStockService stockService
    )
    {
        _transactionsRepo = transactionsRepo;
        _mapper = mapper;
        _transactionService = transactionService;
        _stocksRepo = stocksRepo;
        _userManager = userManager;
        _stockService = stockService;
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
    // uspio si iqueryable, sve dela, ne treba ti ništa drugo!
    [HttpGet("pekismekica")]
    public async Task<ActionResult<IEnumerable<TransactionsForUserVM>>> GetTransactionsForSpecificUser5(
        [FromQuery]QueryParameters queryParameters)
    {
         var email = HttpContext.User.RetrieveEmailFromPrincipal();

         var list = await _transactionService.ShowTransactionsForSpecificUser2(queryParameters, email);

        return Ok(list);
    }
    //ovo koristiš više ne, nego pekidrekimo
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
    // ovo šljaka, najbolje je, to koristi obzirom da možeš filtrirati u services, alternativa je bila pekidreki!
    [HttpGet("pekidrekimo")]
    public async Task<ActionResult<IEnumerable<TransactionsForUserVM>>> GetTransactionsForSpecificUser77(
        [FromQuery]QueryParameters queryParameters)
    {
         var email = HttpContext.User.RetrieveEmailFromPrincipal();

         var list = await _transactionService.ShowTransactionsForSpecificUser4(queryParameters, email);

        return Ok(list);
    }
  
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

    // ovo ti je za buystockreactive!
    [HttpPost("kreativo/{id}")]
    public async Task<ActionResult> CreateTransactionist1(int id, TransactionToCreateVM transactionVM)
    {     
        transactionVM.Email = User.RetrieveEmailFromPrincipal();

        await _transactionService.InitialisingTaxLiability(transactionVM.Email);

        var transaction = new StockTransaction                                                                                                    
        {
             Id = transactionVM.Id,
             Date = transactionVM.BuyingDate,
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
    // ovo ti je za sellstockreactive!
    [HttpPost("kreativissimo/{id}")]
    public async Task<ActionResult> CreateTransactionist2(int id, 
    TransactionToCreateVM transactionVM)
    {
        transactionVM.Email = User.RetrieveEmailFromPrincipal();

       // var userId = await _transactionService.GetUserId();

        if(await _transactionService.TotalQuantity(transactionVM.Email, id) < transactionVM.Quantity)
        {
            // stavio si badrequest kako bi ti prošla ona fora od Felipea
             // return new BadRequestObjectResult
             // (new ApiValidationErrorResponse{Errors = new []{"You are selling more than you have!"}});

             return BadRequest("You are selling more than you have!");
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

        await _transactionService.UpdateTaxLiability(transactionVM.Email);

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

    //ovo koristiš za taxliability!
    [HttpGet("profitwow")]
    public async Task<ActionResult<TaxLiabilityVM>> CheckTotalProfit1()
    {
        var email = User.RetrieveEmailFromPrincipal();

        var taxLiability = await _transactionService.ReturnTaxLiability(email);

        return Ok(taxLiability);

    }
    // ovo ti je za typeahead
    [HttpPut("profitwowy/{id}")]
    public async Task<ActionResult<TaxLiabilityVM>> CheckTotalProfit2(int id)
    {
        var email = User.RetrieveEmailFromPrincipal();

        var taxLiability = await _transactionService.ReturnTaxLiability1(email, id);

        return Ok(taxLiability);

    }
    [HttpPut("profitwowz")]
    public async Task<ActionResult<TaxLiabilityVM>> CheckTotalProfit3(Surtax surtax)
    {
        var email = User.RetrieveEmailFromPrincipal();

        var taxLiability = await _transactionService.ReturnTaxLiability1(email, surtax.Id);

        return Ok(taxLiability);

    }
    // dobio si fifo uz pomoć stackowerflova
    [HttpGet("fifo")]
    public async Task<ActionResult<IEnumerable<StockTransaction>>> GetMeFifo()
    {
        var email = User.RetrieveEmailFromPrincipal();
      //  var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var list = await _transactionService.Fifo5(email);

        var data = _mapper.Map<IEnumerable<StockTransaction>, IEnumerable<TransactionToReturnDto>>(list);

        return Ok(data); 
    }

     [HttpGet("profitwowpeki")]
    public async Task<ActionResult<TaxLiabilityVM>> CheckTotalProfit11()
    {
         // var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
        // string userId1 =  _userManager.GetUserId(HttpContext.User);

        //var user = new AppUser();
        var id = _userManager.GetUserId(User);

        var email = User.RetrieveEmailFromPrincipal();

        var user = await _userManager.FindByEmailAsync(email);

        string userid7 = await _transactionService.GetUserId7(email);

       // var usery = _userManager.FindByIdAsync(user.Id);

       //var userId = user.Id;
            
        var taxLiability = await _transactionService.ReturnTaxLiability7(userid7);

        return Ok(taxLiability);

    }
    [HttpGet("pekismekica1")]
    public async Task<ActionResult<TransactionsForUserListVM>> GetTransactionsForSpecificUser575(
        [FromQuery]QueryParameters queryParameters)
    {
         var email = HttpContext.User.RetrieveEmailFromPrincipal();

         var list = await _transactionService.ShowTransactionsForSpecificUser5(queryParameters, email);

        return Ok(list);
    }
    // uspio si povući userid bez jwt autentikacije (inače trebaš auth, gore na vrhu ti je), očito 
    // trebaš usermanagera!!!!!!!!
    // šljaka ti varijanta i sa jwt, samo u startup moraš u konstruktor sve staviti 
    [HttpGet("ajdevratiga")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> GetUserId()
    {
        // var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
           var email = User.RetrieveEmailFromPrincipal();
           var user = await _userManager.FindByEmailAsync(email);
           var userId = user.Id;

           return Ok(userId);
    }

    // ova dva dolje su ti za kupnju i prodaju uz stvaranje taxliability, sada za probu:)
    // šljakaju za sada, koristiš ih
    [HttpPost("kreativo1/{id}")]
    public async Task<ActionResult> CreateTransactionist11(int id, TransactionToCreateVM transactionVM)
    {     
        transactionVM.Email = User.RetrieveEmailFromPrincipal();

        await _transactionService.InitialisingTaxLiability(transactionVM.Email);

        // ovo stavljaš anual
      //  await _transactionService.CreatingPurchaseNewAnnualProfitOrLoss(transactionVM.Email);


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

    [HttpPost("kreativissimo1/{id}")]
    public async Task<ActionResult> CreateTransactionist22(int id, TransactionToCreateVM transactionVM)
    {
        transactionVM.Email = User.RetrieveEmailFromPrincipal();

        if (await _transactionService.TotalQuantity(transactionVM.Email, id) < transactionVM.Quantity)
        {
             return BadRequest("You are selling more than you have!");
        }

        var transaction = await _transactionService.LetsSellStock(transactionVM, id);
        
        await _transactionService.UpdateResolvedAndLocked(transaction, id, User.RetrieveEmailFromPrincipal());

        await _transactionService.TwoYearException(transactionVM.Email, transaction );

        await _transactionService.UpdateTaxLiabilityIncludingLocked(transactionVM.Email);


       // await _transactionService.CreatingLoginNewAnnualProfitOrLoss(transactionVM.Email);

        return NoContent();
    }    
    [HttpGet("lastelement")]
    public async Task<ActionResult> ReturnLastTransaction()
    {
        var email = User.RetrieveEmailFromPrincipal();

        var lastTransaction = await _transactionService.ReturnLastTranscation(email);

        return Ok(lastTransaction);
    }    
    [HttpGet("profitko")]
    public async Task<ActionResult> ReturnTotalProfit()
    {
        var email = User.RetrieveEmailFromPrincipal();

        var profit = await _transactionService.ReturnTotalNetProfitOrLoss(email);

        return Ok(profit);
    }    
    [HttpGet("bravoo")]
    public async Task<ActionResult> TotalNetProfitForCurrentYear()
    {
        var email = User.RetrieveEmailFromPrincipal();
         
        decimal? profit = await _transactionService.TotalNetProfitForCurrentYear2(email);
        return Ok(profit);
    }    
    // sada ćeš pokušati implementirati cjelokupnu prodaju u jednoj metodi skupa sa exception od 2 godine
    [HttpPost("kreativissimo2/{id}")]
    public async Task<ActionResult> CreateTransaction(int id, TransactionToCreateVM transactionVM)
    {
        var stock = await _stockService.FindStockById(id);

        if (stock == null) return BadRequest();

        transactionVM.Email = User.RetrieveEmailFromPrincipal();

        if (await _transactionService.TotalQuantity(transactionVM.Email, id) < transactionVM.Quantity)
        {
             return BadRequest("You are selling more than you have!");
        }

        var transaction = await _transactionService.LetsSellStock(transactionVM, id);
        
        await _transactionService.UpdateResolvedAndLocked(transaction, id, User.RetrieveEmailFromPrincipal());

        await _transactionService.CreatingSellingNewAnnualProfitOrLoss1(transactionVM.Email, transaction);

        return NoContent();
    }    
  }
}








