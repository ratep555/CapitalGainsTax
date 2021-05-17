using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.ViewModels;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        // private readonly IUnitOfWork _unitOfWork;

        private readonly StoreContext _context;

        public TransactionService(StoreContext context)
        {
            _context = context;


        }
        /*   public async Task<StockTransaction> CreateTransactionAsync(string userId)
          {
              var transaction = new StockTransaction();
              transaction.UserId = userId;

              _unitOfWork.Repository<StockTransaction>().Add(transaction);

              var result = await _unitOfWork.Complete();

              if (result <= 0) return null;

              return transaction;
          } */
        /* public async Task<IReadOnlyList<StockTransaction>> GetTransactionsForUserAsync(string userId)
        {
            var spec = new TransactionsWithStocksSpecification(userId);

            return await _unitOfWork.Repository<StockTransaction>().ListAsync(spec);
        } */
        
        public async Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser(string email)
        {
            var transactions = (from t in _context.StockTransactions
                               join u in _context.Users.Where(u => u.Email == email)                          
                               on t.UserId equals u.Id 
                               join s in _context.Stocks
                               on t.StockId equals s.Id
                               select new TransactionsForUserVM 
                               {
                                    Id = t.Id,
                                    UserId = t.UserId,
                                    StockId = s.Id,
                                    Stock = s.Symbol,
                                    Quantity = t.Quantity,
                                    Purchase = t.Purchase,
                                    Resolved = t.Resolved,
                                    Date = t.Date    
                                    
                             }).AsEnumerable().OrderBy(t => t.Date);          
                                    
               return await Task.FromResult(transactions);
     
        }

        public async Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser1(QueryParameters queryParameters,
         string email)
        {
            var transactions = 
                              (from t in _context.StockTransactions
                               join u in _context.Users.Where(u => u.Email == email)                          
                               on t.UserId equals u.Id 
                               join s in _context.Stocks
                               on t.StockId equals s.Id
                               select new TransactionsForUserVM 
                               {
                                    Id = t.Id,
                                    UserId = t.UserId,
                                    StockId = s.Id,
                                    Stock = s.Symbol,
                                    Quantity = t.Quantity,
                                    Purchase = t.Purchase,
                                    Price = t.Price,
                                    Resolved = t.Resolved,
                                    Date = t.Date    
                                    
                             }).AsEnumerable();         

            if (queryParameters.HasQuery())
            {
            transactions = transactions
                    .Where(t => t.Stock.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }
                                    
               return await Task.FromResult(transactions);
     
        }
        public async Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser3(
        QueryParameters queryParameters,
        string email)
        {
            var transactions = await
                              (from t in _context.StockTransactions
                               join u in _context.Users.Where(u => u.Email == email)                          
                               on t.Email equals u.Email 
                               join s in _context.Stocks
                               on t.StockId equals s.Id
                               select new TransactionsForUserVM 
                               {
                                    Id = t.Id,
                                    StockId = s.Id,
                                    Stock = s.Symbol,
                                    Quantity = t.Quantity,
                                    Purchase = t.Purchase,
                                    Price = t.Price,
                                    Resolved = t.Resolved,
                                    Date = t.Date,
                                    Email = email
                                    
                             }).OrderBy(t => t.Id).ToListAsync();   

              /*  int? basket = 0;
               decimal? basket1 = 0;
               decimal? basket2 = 0;
               decimal? basket3 = 0;
               decimal? basket4 = 0; */
               /* decimal? basket5 = 0;
               decimal? basket6 = 0;
               decimal? basket7 = 0;
               decimal? basket8 = 0; */

               foreach (var item in transactions)
               {
                     var model1 = await _context.StockTransactions.FindAsync(item.Id);
                     
                     if (item.Purchase == true)
                     {
                         if (item.Resolved != 0)
                         {                            
                             item.NetProfit = (model1.Resolved * model1.Price);
                         }
                     }
                     else if (item.Purchase == false)
                     {
                         item.NetProfit = (model1.Quantity * model1.Price);
                     }                                                                                                                           
               }
               
               return await Task.FromResult(transactions);    
        }

        public async Task<IQueryable<TransactionsForUserVM>> ShowTransactionsForSpecificUser2(QueryParameters queryParameters,
         string email)
        {
            IQueryable<TransactionsForUserVM> transactions = 
                              (from t in _context.StockTransactions
                               join u in _context.Users.Where(u => u.Email == email)                          
                               on t.UserId equals u.Id 
                               join s in _context.Stocks
                               on t.StockId equals s.Id
                               select new TransactionsForUserVM 
                               {
                                    Id = t.Id,
                                    UserId = t.UserId,
                                    StockId = s.Id,
                                    Stock = s.Symbol,
                                    Price = t.Price,
                                    Quantity = t.Quantity,
                                    Purchase = t.Purchase,
                                    Resolved = t.Resolved,
                                    Date = t.Date    
                                    
                             }).AsQueryable().OrderBy(s => s.Stock);         

            if (queryParameters.HasQuery())
            {
                    transactions = transactions
                    .Where(t => t.Stock.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }
                                    
               return await Task.FromResult(transactions);
     
        }
        public async Task<StockTransaction> CreateTransaction(StockTransaction transaction)
        {
             _context.StockTransactions.Add(transaction);
             await _context.SaveChangesAsync();

             return transaction;
        }
        public async Task<StockTransaction> CreateTransaction1(
            StockTransaction transaction, 
            int stockId, 
            string email)
        {
             _context.StockTransactions.Add(transaction);
             await _context.SaveChangesAsync();

             int soldQuantity = 0;

             var list = _context.StockTransactions
             .Where(x => x.StockId == stockId && x.Email == email).ToList();

             foreach(var item in list)
             {
                 if(item.Purchase == false)
                 {
                     soldQuantity = soldQuantity + item.Quantity;
                 }
             }

             foreach(var item in list)
             {
                    if(item.Purchase == true)
                    {
                        var model1 = _context.StockTransactions.Where
                        (x => x.Id == item.Id && x.StockId == stockId).FirstOrDefault();
                    
                        if(model1 != null) 
                        {
                                if(soldQuantity > 0)
                                {
                                    var newSoldQuantity = soldQuantity - item.Quantity;

                                    if(newSoldQuantity >= 0)
                                    {
                                        model1.Resolved = item.Quantity;

                                        _context.SaveChanges();
                                    }
                                    else if(newSoldQuantity < 0)
                                    {
                                        model1.Resolved = soldQuantity;

                                        _context.SaveChanges();
                                    }
                                    soldQuantity = newSoldQuantity;
                                }
                        }
                    }                   
             }

             return transaction;
        }
        public async Task<string> GetUserId()
        {
            var userId = await (from u in _context.Users
                         select u.Id).FirstOrDefaultAsync();         

            return await Task.FromResult(userId);         
        } 
        public async Task<StockTransaction> CreateTransaction2(
            StockTransaction transaction, 
            int stockId, 
            string userId)
        {
             _context.StockTransactions.Add(transaction);
             await _context.SaveChangesAsync();

             int soldQuantity = 0;

             var list = _context.StockTransactions
             .Where(x => x.StockId == stockId && x.UserId == userId).ToList();

             foreach(var item in list)
             {
                 if(item.Purchase == false)
                 {
                     soldQuantity = soldQuantity + item.Quantity;
                 }
             }

             foreach(var item in list)
             {
                    if(item.Purchase == true)
                    {
                        var model1 = _context.StockTransactions.Where
                        (x => x.Id == item.Id && x.StockId == stockId).FirstOrDefault();
                    
                        if(model1 != null) 
                        {
                                if(soldQuantity > 0)
                                {
                                    var newSoldQuantity = soldQuantity - item.Quantity;

                                    if(newSoldQuantity >= 0)
                                    {
                                        model1.Resolved = item.Quantity;

                                        _context.SaveChanges();
                                    }
                                    else if(newSoldQuantity < 0)
                                    {
                                        model1.Resolved = soldQuantity;

                                        _context.SaveChanges();
                                    }
                                    soldQuantity = newSoldQuantity;
                                }
                        }
                    }                   
             }

             return transaction;
        }
        public async Task<StockTransaction> CreateTransaction3(
            StockTransaction transaction, 
            int stockId, 
            string email)
        {
             _context.StockTransactions.Add(transaction);
             await _context.SaveChangesAsync();

             int soldQuantity = 0;

             var list = _context.StockTransactions
             .Where(x => x.StockId == stockId && x.Email == email).ToList();

             foreach(var item in list)
             {
                 if(item.Purchase == false)
                 {
                     soldQuantity = soldQuantity + item.Quantity;
                 }
             }

             foreach(var item in list)
             {
                    if(item.Purchase == true)
                    {
                        var model1 = _context.StockTransactions.Where
                        (x => x.Id == item.Id && x.StockId == stockId).FirstOrDefault();
                    
                        if(model1 != null) 
                        {
                                if(soldQuantity > 0)
                                {
                                    var newSoldQuantity = soldQuantity - item.Quantity;

                                    if(newSoldQuantity >= 0)
                                    {
                                        model1.Resolved = item.Quantity;

                                        _context.SaveChanges();
                                    }
                                    else if(newSoldQuantity < 0)
                                    {
                                        model1.Resolved = soldQuantity;

                                        _context.SaveChanges();
                                    }
                                    soldQuantity = newSoldQuantity;
                                }
                        }
                    }                   
             }

             return transaction;
        }
       /*  public async Task<string> GetUserId()
        {
            var userId = await (from u in _context.Users
                         select u.Id).FirstOrDefaultAsync();         

            return await Task.FromResult(userId);         
        }  */
        public async Task<int> TotalQuantity(string email, int stockId)
        {
             int totalQuantity =  (_context.StockTransactions
             .Where(t => t.Email == email && t.StockId == stockId && t.Purchase == true)
             .Sum(t => t.Quantity)) - 
             (_context.StockTransactions
             .Where(t => t.Email == email && t.StockId == stockId && t.Purchase == false)
             .Sum(t => t.Quantity));

             return await Task.FromResult(totalQuantity);
        }
        public async Task<decimal> TotalNetProfit(string email)
        { 
            decimal basket = 0;

            foreach (var item in _context.Stocks.ToList())
            {
                var totalNetProfit = (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false)
                .Sum(t => t.Price * t.Quantity)) - 
                (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == true && t.Resolved > 0)
                .Sum(t => t.Resolved * t.Price));

                basket += totalNetProfit;
            }

            return await Task.FromResult(basket);
        }
        public async Task<TaxLiabilityVM> TotalNetProfit1(string email)
        { 
            decimal? basket4 = 0;
            decimal f = 100;


            var taxLiability = new TaxLiabilityVM();

            foreach (var item in _context.Stocks.ToList())
            {
                var totalNetProfit = (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false)
                .Sum(t => t.Price * t.Quantity)) - 
                (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == true && t.Resolved > 0)
                .Sum(t => t.Resolved * t.Price));

                basket4 += totalNetProfit;

            
                
                     decimal? basket5 =  (12 / f) * basket4;
                     decimal? basket6 = (18 / f) * basket5;
                     decimal? basket7 = basket5 + basket6;
                     decimal? basket8 = basket4 - basket7;

            taxLiability.GrossProfit = basket4;
            taxLiability.CapitalGainsTax = basket5;
            taxLiability.Surtax = basket6;
            taxLiability.TotalTaxLiaility = basket7;
            taxLiability.NetProfit = basket8;
            }

            return await Task.FromResult(taxLiability);        }

        public async Task<TaxLiabilityVM> ReturnTaxLiability(string email)
        {
            decimal f = 100;
            decimal? basket4 = await TotalNetProfit(email);
            decimal? basket5 = (12 / f) * basket4;
            decimal? basket6 = (18 / f) * basket5;
            decimal? basket7 = basket5 + basket6;
            decimal? basket8 = basket4 - basket7;

            var taxLiability = new TaxLiabilityVM
            {
                GrossProfit = basket4,
                CapitalGainsTax = basket5,
                Surtax = basket6,
                TotalTaxLiaility = basket7,
                NetProfit = basket8
            };

            return await Task.FromResult(taxLiability);
        }
    }
}









