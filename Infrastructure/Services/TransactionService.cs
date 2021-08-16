using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.ViewModels;
using Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

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
        public async Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser4(
        QueryParameters queryParameters,
        string email)
        {
            IEnumerable<TransactionsForUserVM> transactions = 
                             await (from t in _context.StockTransactions
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
                                    
                             }).OrderBy(x => x.Date).ToListAsync();   


            if (queryParameters.HasQuery())
            {
                     transactions =  transactions
                               .Where(t => t.Stock.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }
                         
              /*  foreach (var item in transactions)
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
               } */
               
               return await Task.FromResult(transactions);    
        }

        public async Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser2(QueryParameters queryParameters,
         string email)
        {
            IQueryable<TransactionsForUserVM> transactions =
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
                                    
                             }).AsQueryable().OrderBy(s => s.Date);         

            if (queryParameters.HasQuery())
            {
                    transactions = transactions
                    .Where(t => t.Stock.Contains(queryParameters.Query));
            }
                                    
               return await transactions.ToListAsync();
     
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

             var list = await _context.StockTransactions
             .Where(x => x.StockId == stockId && x.Email == email).ToListAsync();

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
                        var model1 = await _context.StockTransactions.Where
                        (x => x.Id == item.Id && x.StockId == stockId).FirstOrDefaultAsync();
                    
                        if(model1 != null) 
                        {
                                if(soldQuantity > 0)
                                {
                                    var newSoldQuantity = soldQuantity - item.Quantity;

                                    if(newSoldQuantity >= 0)
                                    {
                                        model1.Resolved = item.Quantity;

                                        await _context.SaveChangesAsync();
                                    }
                                    else if(newSoldQuantity < 0)
                                    {
                                        model1.Resolved = soldQuantity;

                                        await _context.SaveChangesAsync();
                                    }
                                    soldQuantity = newSoldQuantity;
                                }
                        }
                    }                   
             }

             return await Task.FromResult(transaction);
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

             var list = await _context.StockTransactions
             .Where(x => x.StockId == stockId && x.UserId == userId).ToListAsync();

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
                        var model1 = await _context.StockTransactions.Where
                        (x => x.Id == item.Id && x.StockId == stockId).FirstOrDefaultAsync();
                    
                        if(model1 != null) 
                        {
                                if(soldQuantity > 0)
                                {
                                    var newSoldQuantity = soldQuantity - item.Quantity;

                                    if(newSoldQuantity >= 0)
                                    {
                                        model1.Resolved = item.Quantity;

                                       await _context.SaveChangesAsync();
                                    }
                                    else if(newSoldQuantity < 0)
                                    {
                                        model1.Resolved = soldQuantity;

                                        await _context.SaveChangesAsync();
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

             var list = await _context.StockTransactions
             .Where(x => x.StockId == stockId && x.Email == email).ToListAsync();

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
                        var model1 = await _context.StockTransactions.Where
                        (x => x.Id == item.Id && x.StockId == stockId).FirstOrDefaultAsync();
                    
                        if(model1 != null) 
                        {
                                if(soldQuantity > 0)
                                {
                                    var newSoldQuantity = soldQuantity - item.Quantity;

                                    if(newSoldQuantity >= 0)
                                    {
                                        model1.Resolved = item.Quantity;

                                       await _context.SaveChangesAsync();
                                    }
                                    else if(newSoldQuantity < 0)
                                    {
                                        model1.Resolved = soldQuantity;

                                       await _context.SaveChangesAsync();
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
       
        public async Task<TaxLiabilityVM> ReturnTaxLiability1(string email, int surtaxId)
        {
            var surtax = await _context.Surtaxes.Where(s => s.Id == surtaxId).FirstOrDefaultAsync();
            
            decimal f = 100;
            decimal? basket4 = await TotalNetProfit(email);
            decimal? basket5 = (12 / f) * basket4;
            decimal? basket6 = (surtax.Amount / f) * basket5;
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
        public async Task<StockTransaction> GetTransactionByEmailAndId(string email, int stockId, int quantity)
        {
            var transaction = await _context.StockTransactions.Where(t => t.Email == email && t.StockId == stockId && t.Quantity == quantity)
            .FirstOrDefaultAsync();

            return await Task.FromResult(transaction);
        }
        public async Task<Stock> FindStockById(int id)
        {
            return await _context.Stocks.Where(s => s.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<StockTransaction>> Fifo(string email)
        {
            var today = DateTime.Now.AddDays(2);

            var list = await  _context.StockTransactions.Where(o => (o.Email == email)).ToListAsync();

            list = list.Where((o => (o.Purchase == true && o.Quantity != o.Resolved) && (
            o.Purchase == false && o.Date > today))).ToList();

            return await Task.FromResult(list);
        }
        public async Task<IEnumerable<StockTransaction>> Fifo1(string email)
        {
            var today = DateTime.Now.AddDays(2); 

            var list = await  _context.StockTransactions.Where(o => (o.Email == email)).ToListAsync();

            list = list.Where(o => o.Purchase == true && o.Quantity != o.Resolved).ToList();

            return await Task.FromResult(list);
        }

        public async Task<IEnumerable<StockTransaction>> Again(string email)
        {
            var datesy = DateTime.Now.AddDays(2);

            var list = await (from t in _context.StockTransactions
                       where t.Email == email
                       select t).ToListAsync();

            var list1 = (from s in list
                        where s.Purchase == true && s.Quantity != s.Resolved
                        where s.Purchase != true && s.Date > datesy
                        select s).ToList();

            return await Task.FromResult(list1);
        }


        public async Task<string> GetUserId1()
    {
        var userId = await (from u in _context.Users
                            select u.Id).FirstOrDefaultAsync();

        return await Task.FromResult(userId);
    }

     public async Task<IEnumerable<StockTransaction>> Fifo5(string email)
    {
            var today = DateTime.Now.AddDays(2);

            var list = await  _context.StockTransactions.Include(s => s.Stock)
            .Where(o => o.Email == email && 
            (o.Purchase == true && o.Quantity != o.Resolved) || (
            o.Purchase == false && o.Date > today)).ToListAsync();

            return await Task.FromResult(list);
    }

     private async Task<decimal> TotalNetProfit5(string userId)
        { 
            decimal basket = 0;

            foreach (var item in _context.Stocks.ToList())
            {
                var totalNetProfit = (_context.StockTransactions
                .Where(t => t.UserId == userId && t.StockId == item.Id && t.Purchase == false)
                .Sum(t => t.Price * t.Quantity)) - 
                (_context.StockTransactions
                .Where(t => t.UserId == userId && t.StockId == item.Id && t.Purchase == true && t.Resolved > 0)
                .Sum(t => t.Resolved * t.Price));

                basket += totalNetProfit;
            }

            return await Task.FromResult(basket);
        }

           public async Task<TaxLiabilityVM> ReturnTaxLiability7(string userId)
        {
            decimal f = 100;
            decimal? basket4 = await TotalNetProfit5(userId);
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

        public async Task<string> GetUserId7(string email)
        {
            var userId = await (from u in _context.Users.Where(u => u.Email == email)
                         select u.Id).FirstOrDefaultAsync();         

            return await Task.FromResult(userId);         
        } 

         public async Task<TransactionsForUserListVM> ShowTransactionsForSpecificUser5(QueryParameters queryParameters,
         string email)
        {
            TransactionsForUserListVM list = new TransactionsForUserListVM();

            IQueryable<TransactionsForUserVM> transactions =
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
                                    Email = email,
                                    NetProfit = t.Quantity * t.Price                                    
                             }).AsQueryable().OrderBy(s => s.Date);         

            if (queryParameters.HasQuery())
            {
                    transactions = transactions
                    .Where(t => t.Stock.Contains(queryParameters.Query));
            }
            list.ListOfTransactions = transactions;

            decimal? basket1 = 0;
            decimal? basket2 = 0;
            decimal? basket3 = 0;

            foreach (var item in transactions)
            {
                basket1 += item.NetProfit;
            }

            foreach (var item in transactions)
            {
                 if (item.Purchase == false)
                 {
                     basket2 += item.NetProfit;                    
                 }
            }          
            foreach (var item in transactions)
            {
                 if (item.Purchase == true && item.Resolved > 0)
                 {
                     basket3 += (item.Price * item.Resolved);
                 }
            }          
           
           // list.TotalNetProfit = await TotalNetProfit7(email);
            list.TotalNetProfit1 = basket2 - basket3;
          //  list.TotalTraffic = await TotalTraffic(email);
            list.TotalTraffic1 = basket1;

               return await Task.FromResult(list);
     
        }
        public async Task InitializeTaxLiability(string email)
        {          
            var taxLiability = new TaxLiability
            {
                Email = email,
                GrossProfit = 0,
                CapitalGainsTax = 0,
                SurtaxId = 1,
                SurtaxAmount = 0,
                TotalTaxLiaility = 0,
                NetProfit = 0,
                Locked = false
            };
            _context.TaxLiabilities.Add(taxLiability);
            
            await _context.SaveChangesAsync();           
        }
        public async Task InitializeAnnualProfitOrLoss(string email)
        {          
             var taxcard1 = new AnnualProfitOrLoss
                    {
                        Year = DateTime.Now.Year,
                        Amount = 0,
                        Email = email,
                        Locked = false
                    };

                    _context.AnnualProfitsOrLosses.Add(taxcard1);
                    await _context.SaveChangesAsync();       
        }
        public async Task UpdateTaxLiability(string email)
        {      
            var taxLiability = await _context.TaxLiabilities.Include(s => s.Surtax)
            .Where(t => t.Email == email).FirstOrDefaultAsync();
            
            var user = await _context.AppUsers.Where(ap => ap.Email == email)
                       .FirstOrDefaultAsync();

            var surtax = await _context.Surtaxes.Where(s => s.Id == user.SurtaxId)
                         .FirstOrDefaultAsync();

            decimal f = 100;
            decimal? basket4 = await TotalNetProfit(email);
            decimal? basket5 = (12 / f) * basket4;

            taxLiability.SurtaxId = surtax.Id;
            taxLiability.Year = DateTime.Now.Year;
            taxLiability.Email = email;    
            taxLiability.GrossProfit = basket4;

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.CapitalGainsTax = basket5;
            }
            else
            {
                taxLiability.CapitalGainsTax = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.SurtaxAmount = (surtax.Amount / f) * basket5;
            }
            else
            {
                taxLiability.SurtaxAmount = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.TotalTaxLiaility = basket5 + taxLiability.SurtaxAmount;
            }
            else
            {
                taxLiability.TotalTaxLiaility = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.NetProfit = basket4 - taxLiability.TotalTaxLiaility;
            }
            else
            {
                taxLiability.NetProfit = 0;
            }

            _context.Entry(taxLiability).State = EntityState.Modified;        
            await _context.SaveChangesAsync();         
        }
       
        public async Task<TaxLiabilityVM> ReturnTaxLiability77(string email)
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

        public async Task InitialisingTaxLiability(string email)
        {
            if (!_context.StockTransactions.Where(x => x.Email == email).Any())
            {
                await InitializeTaxLiability(email);
                //ovo sve dolje si dodao za annual
                await InitializeAnnualProfitOrLoss(email);                
            }
                await CreatingPurchaseNewAnnualProfitOrLoss(email);

        }

        public async Task<IEnumerable<StockTransaction>> GetListOftransactionsByEmail( string email)
        {
            return await _context.StockTransactions.Where(t => t.Email == email)
                   .ToListAsync();
        }

        public async Task CreatingNewStockTransaction(
            StockTransaction transaction, 
            int stockId, 
            string email)
        {

             int soldQuantity = 0;

             var list = await _context.StockTransactions
             .Where(x => x.StockId == transaction.StockId && x.Email == email).ToListAsync();

             foreach(var item in list)
            {
                 if(item.Locked != true && item.Purchase == false)
                 {
                     soldQuantity = soldQuantity + item.Quantity;
                 }
            }

             foreach(var item in list)
            {
                 if(item.Locked != true)
                {
                    if(item.Purchase == true)
                    {
                        var model1 = await _context.StockTransactions.Where
                        (x => x.Id == item.Id && x.StockId == stockId).FirstOrDefaultAsync();
                    
                        if(model1 != null) 
                        {
                                if(soldQuantity > 0)
                                {
                                    var newSoldQuantity = soldQuantity - item.Quantity;

                                    if(newSoldQuantity >= 0)
                                    {
                                        model1.Resolved = item.Quantity;
                                        model1.Locked = true;
                                        
                                        _context.Entry(model1).State = EntityState.Modified;
                                        await _context.SaveChangesAsync();
                                    }
                                    else if(newSoldQuantity < 0)
                                    {
                                        model1.Resolved = soldQuantity;

                                        _context.Entry(model1).State = EntityState.Modified;
                                        await _context.SaveChangesAsync();
                                    }
                                    soldQuantity = newSoldQuantity;
                                }
                        }
                    }                   
                }

                if (item.Purchase == false)
                {
                        var model2 = await _context.StockTransactions.Where
                        (x => x.Id == item.Id && x.StockId == stockId).FirstOrDefaultAsync();

                        model2.Locked = true;
                        _context.Entry(model2).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                }
            }           
        }
    
        public async Task<StockTransaction> ReturnLastTranscation( string email)
        {

            var transaction = await _context.StockTransactions.Where(t => t.Email == email).
                   OrderBy(t => t.Date) .LastOrDefaultAsync();

            var today = transaction.Date.Year;

            return transaction;
        }

        public async Task CreateNewTaxLiabilityUponNewYear(string email)
        {
            var taxliability = await _context.TaxLiabilities.Where(t => t.Email == email && t.Locked == false)
                               .FirstOrDefaultAsync();

             var transaction = await _context.StockTransactions.Where(t => t.Email == email).
                               OrderBy(t => t.Date) .LastOrDefaultAsync();

            var today = transaction.Date.Year;

        if (DateTime.Now.Year > today)
        {
                taxliability.Locked = true;
                await _context.SaveChangesAsync();

            var taxLiability = new TaxLiability
            {
                Email = email,
                Year = DateTime.Now.Year,
                GrossProfit = 0,
                CapitalGainsTax = 0,
                SurtaxId = taxliability.SurtaxId,
                SurtaxAmount = 0,
                TotalTaxLiaility = 0,
                NetProfit = 0,
                Locked = false
            };

            _context.TaxLiabilities.Add(taxLiability);
            
            await _context.SaveChangesAsync();   
        }
        else
        {
            await UpdateTaxLiabilityIncludingLocked(email);
        }
           
        }
        public async Task UpdateTaxLiabilityIncludingLocked(string email)
        {      
            var taxLiability = await _context.TaxLiabilities.Include(s => s.Surtax)
            .Where(t => t.Email == email && t.Locked != true).FirstOrDefaultAsync();
            
            var user = await _context.AppUsers.Where(ap => ap.Email == email)
                       .FirstOrDefaultAsync();

            var surtax = await _context.Surtaxes.Where(s => s.Id == user.SurtaxId)
                         .FirstOrDefaultAsync();

            decimal f = 100;
            decimal? basket4 = await TotalNetProfit(email);
            decimal? basket5 = (12 / f) * basket4;

            taxLiability.SurtaxId = surtax.Id;
            taxLiability.Year = DateTime.Now.Year;
            taxLiability.Email = email;    
            taxLiability.GrossProfit = basket4;

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.CapitalGainsTax = basket5;
            }
            else
            {
                taxLiability.CapitalGainsTax = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.SurtaxAmount = (surtax.Amount / f) * basket5;
            }
            else
            {
                taxLiability.SurtaxAmount = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.TotalTaxLiaility = basket5 + taxLiability.SurtaxAmount;
            }
            else
            {
                taxLiability.TotalTaxLiaility = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.NetProfit = basket4 - taxLiability.TotalTaxLiaility;
            }
            else
            {
                taxLiability.NetProfit = 0;
            }

            _context.Entry(taxLiability).State = EntityState.Modified;        
            await _context.SaveChangesAsync();  

            // sada dodaješ kod za annual
               await    CreatingSellingNewAnnualProfitOrLoss(email);
        }       

        public async Task<StockTransaction> LetsSellStock(TransactionToCreateVM transactionVM, int id)
        {
            var transaction = new StockTransaction 
            {
             Id = transactionVM.Id,
             Date = DateTime.Now,
             StockId = id,
             Purchase = false,
             Quantity = transactionVM.Quantity,
             Price = transactionVM.Price,
             Resolved = transactionVM.Resolved,
             Email = transactionVM.Email,
             Locked = true
             
            };

            _context.StockTransactions.Add(transaction);
             await _context.SaveChangesAsync();

             return transaction;
        }
         public async Task<StockTransaction> UpdateResolvedAndLocked(
            StockTransaction transaction, 
            int stockId, 
            string email)
        {

            int soldQuantity = 0;

            var list = await _context.StockTransactions
            .Where(x => x.StockId == stockId && x.Email == email).ToListAsync();

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
                        var model1 = await _context.StockTransactions.Where
                        (x => x.Id == item.Id && x.StockId == stockId).FirstOrDefaultAsync();
                    
                        if(model1 != null) 
                        {
                                if(soldQuantity > 0)
                                {
                                    var newSoldQuantity = soldQuantity - item.Quantity;

                                    if(newSoldQuantity >= 0)
                                    {
                                        model1.Resolved = item.Quantity;

                                        await _context.SaveChangesAsync();
                                    }
                                    else if(newSoldQuantity < 0)
                                    {
                                        model1.Resolved = soldQuantity;

                                        await _context.SaveChangesAsync();
                                    }
                                    soldQuantity = newSoldQuantity;
                                }
                        }
                    }                   
            }

            foreach (var item in list)
            {
                if (item.Purchase == true )
                {
                      if (item.Quantity == item.Resolved)
                      {
                          item.Locked = true;
                          _context.Entry(item).State = EntityState.Modified;
                          await _context.SaveChangesAsync();
                      }
                }
            }
             return await Task.FromResult(transaction);
        }

        public async Task<AnnualProfitOrLoss> ReturnTotalNetProfitOrLoss(string email)
        {
            var totalNetProfit = new AnnualProfitOrLoss
            {
                Email = email,
                Year = DateTime.Now.Year,
                Amount = await TotalNetProfit(email)
            };

            return totalNetProfit;
        }

        public async Task CreatingLoginNewAnnualProfitOrLoss(string email)
        {
            var user = await _context.AppUsers.Where(u => u.Email == email).FirstOrDefaultAsync();
            var taxcard = await _context.AnnualProfitsOrLosses.Where(a => a.Email == email && a.Locked == false)
                          .FirstOrDefaultAsync();

            if (_context.AnnualProfitsOrLosses.Where(x => x.Email == email && x.Locked == false).Any())
            {
                if (DateTime.Now.Year > taxcard.Year)
                {
                    taxcard.Locked = true;
                    _context.Entry(taxcard).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    var taxcard1 = new AnnualProfitOrLoss
                    {
                        Year = DateTime.Now.Year,
                        Amount = 0,
                        Email = email,
                        Locked = false
                    };

                    _context.AnnualProfitsOrLosses.Add(taxcard1);
                    await _context.SaveChangesAsync();
                }                
            }
        }
        public async Task CreatingPurchaseNewAnnualProfitOrLoss(string email)
        {
            var user = await _context.AppUsers.Where(u => u.Email == email).FirstOrDefaultAsync();
            var taxcard = await _context.AnnualProfitsOrLosses.Where(a => a.Email == email && a.Locked == false).FirstOrDefaultAsync();

          /*   if (!_context.StockTransactions.Where(x => x.Email == email).Any())
            {
                    var taxcard1 = new AnnualProfitOrLoss
                    {
                        Year = DateTime.Now.Year,
                        Amount = 0,
                        Email = email,
                        Locked = false
                    };

                    _context.AnnualProfitsOrLosses.Add(taxcard1);
                    await _context.SaveChangesAsync();
            } */
      
            if (_context.AnnualProfitsOrLosses.Where(x => x.Email == email && x.Locked == false).Any())
                           
            {
                if (DateTime.Now.Year > taxcard.Year)
                   {
                    taxcard.Locked = true;
                    _context.Entry(taxcard).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    var taxcard2 = new AnnualProfitOrLoss
                    {
                        Year = DateTime.Now.Year,
                        Amount = 0,
                        Email = email,
                        Locked = false
                    };

                    _context.AnnualProfitsOrLosses.Add(taxcard2);
                    await _context.SaveChangesAsync();
                   }
            }
        }
        public async Task CreatingSellingNewAnnualProfitOrLoss(string email)
        {
            var user = await _context.AppUsers.Where(u => u.Email == email).FirstOrDefaultAsync();
            var taxcard = await _context.AnnualProfitsOrLosses.Where(a => a.Email == email && a.Locked == false).FirstOrDefaultAsync();

            if (DateTime.Now.Year == taxcard.Year)
            {
                 taxcard.Year = DateTime.Now.Year;
                 taxcard.Email = email;
                 taxcard.Amount = await TotalNetProfitForCurrentYear2(email);
              /*   if (taxcard.TaxExemption.HasValue && taxcard.TaxExemption > 0)
                {
                     taxcard.TaxExemption = await TwoYearException1(email);
                }
                else
                {
                    taxcard.TaxExemption = 0;
                }
 */
                 taxcard.Locked = false;
            }

            _context.Entry(taxcard).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            if (DateTime.Now.Year > taxcard.Year)
                {
                    taxcard.Locked = true;
                    _context.Entry(taxcard).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    var taxcard2 = new AnnualProfitOrLoss
                    {
                        Year = DateTime.Now.Year,
                        Amount = 0,
                        TaxableIncome = 0,
                        TaxExemption = 0,
                        Email = email,
                        Locked = false
                    };

                    _context.AnnualProfitsOrLosses.Add(taxcard2);
                    await _context.SaveChangesAsync();
                }          
        }        
        public async Task<int> FindSurtaxIdForZagreb()
        {
            int surtaxId =  _context.Surtaxes.Where(s => s.Residence == "Zagreb")
                           .FirstOrDefaultAsync().Id;

            return await Task.FromResult(surtaxId);
        }
        public async Task<decimal?> TotalNetProfitForCurrentYear(string email)
        { 
            var annualProfit = await _context.AnnualProfitsOrLosses
                               .Where(x => x.Email == email && x.Locked == false)
                               .FirstOrDefaultAsync();
            
            int currentYear = annualProfit.Year;

            decimal? basket = 0;
            decimal? basket1 = 0;
            decimal? basket3 = 0;
            int? count = 0;
           
            foreach (var item in _context.Stocks.ToList())
            {
                 var totalNetProfit = (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false && t.Date.Year == currentYear)
                .Sum(t => t.Price * t.Quantity)) - 
                (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == true && t.Resolved > 0 && t.Date.Year == currentYear)
                .Sum(t => t.Resolved * t.Price));

                basket += totalNetProfit;

                count = (_context.StockTransactions
                        .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false 
                        && t.Date.Year == currentYear). Sum(t => t.Quantity));

                basket3 += count;
                
                 var listOfLeftovers = await _context.StockTransactions.Where(x => x.Email == email 
                                     && x.StockId == item.Id &&
                                     x.Purchase == true && x.Date.Year < currentYear 
                                     && x.Quantity != x.Resolved).ToListAsync();

                if (_context.StockTransactions.Where(x => x.Email == email && x.StockId == item.Id &&
                     x.Purchase == true && x.Date.Year < currentYear && x.Quantity != x.Resolved).Any())
                {
                    foreach (var subitem in listOfLeftovers) 
                    {
                        var totalsubprofit = (_context.StockTransactions
                                           .Where(t => t.Email == email && t.StockId == item.Id 
                                           && t.Purchase == true)
                                           .Sum(t => t.Price * count));

                        basket1 += totalsubprofit;
                    }               
                }
              
            }

            decimal? basket2 = basket - basket1;

            return await Task.FromResult(basket2);
        }
        public async Task<decimal?> TotalNetProfitForCurrentYear1(string email)
        { 
            var annualProfit = await _context.AnnualProfitsOrLosses
                               .Where(x => x.Email == email && x.Locked == false)
                               .FirstOrDefaultAsync();
            
            int currentYear = annualProfit.Year;

            decimal? basket = 0;
            decimal? basket1 = 0;
           
            int num = 0;
            int num1 = 0;
           
            foreach (var item in _context.Stocks.ToList())
            {
                
                
                var listOfLeftovers = await _context.StockTransactions.Where(x => x.Email == email 
                                     && x.StockId == item.Id && x.Date.Year < currentYear &&  x.Resolved != 0)
                                     .OrderByDescending(x => x.Id).ToListAsync();

                
                num = (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false && t.Date.Year == currentYear)
                .Sum(t => t.Quantity)) - 
                (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == true && t.Resolved > 0 && t.Date.Year == currentYear)
                .Sum(t => t.Resolved));

                num1 = _context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false && t.Date.Year == currentYear)
                .Sum(t => t.Quantity);

                
                 
                decimal pomozibože = SumOfLastTransaction1(listOfLeftovers, num1);

                var totalNetProfit = (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false && t.Date.Year == currentYear)
                .Sum(t => t.Price * t.Quantity)) - 
                ((_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == true && t.Resolved > 0 && t.Date.Year == currentYear)
                .Sum(t => t.Resolved * t.Price)));

                basket += totalNetProfit;
                              
            }

            decimal? basket2 = basket - basket1;

            return await Task.FromResult(basket);
        }
        private decimal SumOfLastTransaction1(IEnumerable<StockTransaction> stockTransactions, int max)
        {
            decimal result = 0;
            int sum = 0;
            foreach(var stockTransaction in stockTransactions. OrderBy(x => x.Id))
            {
            
                if(sum + stockTransaction.Quantity <= max)
                {
                      result += stockTransaction.Quantity * stockTransaction.Price;
                      sum += stockTransaction.Quantity;
                }
                else
                {
                       result += (max - sum) * stockTransaction.Price;
                       return result;
                }
            }

            return result;
        }
    // ovo dolje ti daje fifo!
      public async Task<decimal?> TotalNetProfitForCurrentYear2(string email)
        { 
            var annualProfit = await _context.AnnualProfitsOrLosses
                               .Where(x => x.Email == email && x.Locked == false)
                               .FirstOrDefaultAsync();
            
            int currentYear = annualProfit.Year;

            decimal? basket = 0;
            decimal? basket2 = 0;
           
            int num = 0;
            int num1 = 0;
           
            foreach (var item in _context.Stocks.ToList())
            {                               
                var listOfLeftovers = await _context.StockTransactions.Where(x => x.Email == email 
                                     && x.StockId == item.Id &&
                                       x.Resolved != 0 && x.Purchase == true)
                                     .OrderByDescending(x => x.Id).ToListAsync();
               
                num = (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false && t.Date.Year == currentYear)
                .Sum(t => t.Quantity)) - 
                (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == true && t.Resolved > 0 && t.Date.Year == currentYear)
                .Sum(t => t.Resolved));

                num1 = _context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false 
                && t.Date.Year == currentYear)
                .Sum(t => t.Quantity);

                
                
                decimal pomozibože = SumOfLastTransaction1(listOfLeftovers, num1);
                basket2 += pomozibože;

                var totalNetProfit = (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false && t.Date.Year == currentYear)
                .Sum(t => t.Price * t.Quantity)) - pomozibože
                ;

                basket += totalNetProfit;                         
            }
            return await Task.FromResult(basket);
        }
        // probati ćeš dolje exception za 2 godine
        public async Task<decimal?> TotalNetProfitForCurrentYear3(string email, StockTransaction transaction)
        { 
            var annualProfit = await _context.AnnualProfitsOrLosses
                               .Where(x => x.Email == email && x.Locked == false)
                               .FirstOrDefaultAsync();
            
            int currentYear = annualProfit.Year;

            decimal? basket = 0;           
            int num1 = 0;

            var list = _context.Stocks.ToList();
           
            foreach (var item in list)
            {                
                var listOfLeftovers = await _context.StockTransactions.Where(x => x.Email == email 
                                     && x.StockId == item.Id &&
                                       x.Resolved != 0 && x.Purchase == true)
                                     .OrderByDescending(x => x.Id).ToListAsync();

                num1 = _context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false && t.Date.Year == currentYear)
                .Sum(t => t.Quantity);
                
                decimal pomozibože = SumOfLastTransaction1(listOfLeftovers, num1);

                var totalNetProfit = (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false && t.Date.Year == currentYear)
                .Sum(t => t.Price * t.Quantity)) - pomozibože;

                basket += totalNetProfit;    

                // sada dodaješ exception za 2 godine                        

                var today = transaction.Date;

                var transactionOlderThenTwoyears = await _context.StockTransactions
                                             .Where(x => x.Email == transaction.Email && x.StockId == transaction.StockId 
                                             && x.Purchase == true && x.Quantity != x.Resolved /* && x.Date < today.AddYears(-2) */)
                                            .LastOrDefaultAsync();

                if (transactionOlderThenTwoyears.Date < transaction.Date.AddYears(-2))
                {
                    decimal last3 = _context.StockTransactions.Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                    .OrderByDescending(x => x.Date).Take(1).Select(x => x.Quantity * x.Price).Sum();

                    int last5 = _context.StockTransactions.Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                    .OrderByDescending(x => x.Date).Take(1).Select(x => x.Quantity).Sum();

                    var list1 = _context.StockTransactions.Where(x => x.Purchase == true && x.Resolved > 0);

                    decimal last4 = SumOfLastTransaction1(list1, last5);

                    totalNetProfit = (_context.StockTransactions
                                      .Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                                      .Sum(x => x.Price * x.Quantity) - last3) -                               
                                      (_context.StockTransactions.Where
                                      (x => x.Email  == email && x.StockId == item.Id && x.Purchase == true && x.Resolved > 0)
                                      .Sum(x => x.Price * x.Resolved) - last4);

                    basket += totalNetProfit;    
                }
            }
            return basket;
        }


        public async Task CreatingSellingNewAnnualProfitOrLoss1(string email, StockTransaction transaction)
        {
            var user = await _context.AppUsers.Where(u => u.Email == email).FirstOrDefaultAsync();
            var taxcard = await _context.AnnualProfitsOrLosses.Where(a => a.Email == email && a.Locked == false).FirstOrDefaultAsync();

            if (DateTime.Now.Year == taxcard.Year)
            {
                 taxcard.Year = DateTime.Now.Year;
                 taxcard.Email = email;
                 taxcard.Amount = await TotalNetProfitForCurrentYear3(email, transaction);
                 taxcard.Locked = false;
            }

            _context.Entry(taxcard).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            if (DateTime.Now.Year > taxcard.Year)
                {
                    taxcard.Locked = true;
                    _context.Entry(taxcard).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    var taxcard2 = new AnnualProfitOrLoss
                    {
                        Year = DateTime.Now.Year,
                        Amount = 0,
                        Email = email,
                        Locked = false
                    };

                    _context.AnnualProfitsOrLosses.Add(taxcard2);
                    await _context.SaveChangesAsync();
                }          
        }      
        //ponovo ćeš probati exception dodati na kraju u kreativissimo1  
    
        public async Task TwoYearException(string email, StockTransaction transaction)
        {
            var annual = await _context.AnnualProfitsOrLosses.Where(x => x.Email == email && x.Locked == false)
                         .FirstOrDefaultAsync();

            decimal basket = 0;
            decimal basket1 = 0;
            var today = transaction.Date;

            var transactionOlderThenTwoyears = await _context.StockTransactions
                                              .Where(x => x.Email == transaction.Email && x.StockId == transaction.StockId 
                                              && x.Purchase == true && x.Resolved > 0).OrderByDescending(x => x.Id)
                                              .FirstOrDefaultAsync();

            foreach (var item in _context.Stocks.Where(x => x.Id == transaction.StockId).ToList())
            {
                  if (transactionOlderThenTwoyears.Date < transaction.Date.AddYears(-2))
                  {
                    decimal last3 = _context.StockTransactions.Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                                .OrderByDescending(x => x.Date).Take(1).Select(x => x.Quantity * x.Price).Sum();

                    int last5 = _context.StockTransactions.Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                            .OrderByDescending(x => x.Date).Take(1).Select(x => x.Quantity).Sum();

                    var list1 = _context.StockTransactions.Where(x => x.Purchase == true && x.Resolved > 0 && x.StockId == item.Id);

                    decimal last4 = SumOfLastTransaction1(list1, last5);

                    var totalNetProfit = _context.StockTransactions
                                      .Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                                      .Sum(x => x.Price * x.Quantity) -                               
                                      _context.StockTransactions.Where
                                      (x => x.Email  == email && x.StockId == item.Id && x.Purchase == true && x.Resolved > 0)
                                      .Sum(x => x.Price * x.Resolved) - (last3 - last4);

                   // basket += totalNetProfit;  
                    basket += (last3 - last4);  
                    basket1 += totalNetProfit;
                 }
            }  
            annual.TaxExemption = basket;
           // annual.TaxableIncome = annual.Amount - annual.TaxExemption;
             _context.Entry(annual).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<decimal?> TwoYearException1(string email)
        {
            var annual = await _context.AnnualProfitsOrLosses.Where(x => x.Email == email && x.Locked == false)
                         .FirstOrDefaultAsync();

            decimal? basket = 0;

            foreach (var item in _context.Stocks.ToList())
            {
                /*  decimal last3 = _context.StockTransactions.Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                                .OrderByDescending(x => x.Date).Take(1).Select(x => x.Quantity * x.Price).Sum();

                    int last5 = _context.StockTransactions.Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                            .OrderByDescending(x => x.Date).Take(1).Select(x => x.Quantity).Sum(); */

                             var lastselling = await _context.StockTransactions
                                  .Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                                  .OrderByDescending(x => x.Id). LastOrDefaultAsync();

                var transactionOlderThenTwoyears = await _context.StockTransactions
                                              .Where(x => x.Email == email && x.StockId == lastselling.StockId
                                              && x.Purchase == true && x.Resolved > 0).OrderByDescending(x => x.Id)
                                              .LastOrDefaultAsync();

               
                  if (transactionOlderThenTwoyears.Date < lastselling.Date.AddYears(-2))
                  {
                    var list = await _context.StockTransactions
                               .Where(x => x.StockId == item.Id && x.Purchase == true && x.Email == email && x.Resolved > 0)
                               .OrderByDescending(x => x.Id).ToListAsync();

                    var numberofselling = lastselling.Quantity;
                    var priceofselling = _context.StockTransactions.Where(x => x.Email == email && x.StockId == item.Id && x.Purchase == false)
                                .OrderByDescending(x => x.Date).Take(1).Select(x => x.Quantity * x.Price).Sum();

                    decimal priceofpurchase = SumOfLastTransaction1(list, numberofselling);
                    decimal total = priceofselling - priceofpurchase;

                    basket += priceofselling;
                 }
            }  
            return basket;           
        }
      
     }
}
                      
        
    










