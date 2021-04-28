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
         public string GetUserId()
         {
            var userId = (from u in _context.Users
                         select u.Id).FirstOrDefault();         

            return userId;         
         } 
    }
}









