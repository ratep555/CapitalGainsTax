using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.ViewModels;

namespace Core.Interfaces
{
    public interface ITransactionService
    {
        // Task<StockTransaction> CreateTransactionAsync(string userId);
       // Task<IReadOnlyList<StockTransaction>> GetTransactionsForUserAsync(string userId);
        Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser(string email);
        Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser1(QueryParameters queryParameters, string email);
        Task<IQueryable<TransactionsForUserVM>> ShowTransactionsForSpecificUser2(QueryParameters queryParameters, string email);

        Task<StockTransaction> CreateTransaction(StockTransaction transaction);
        Task<StockTransaction> CreateTransaction1(
            StockTransaction transaction, 
            int stockId, 
            string userId);
        Task<StockTransaction> CreateTransaction3(
            StockTransaction transaction, 
            int stockId, 
            string email);

        Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser3(
        QueryParameters queryParameters,
        string email);
        Task<string> GetUserId();
        Task<int> TotalQuantity(string userId, int stockId);
        Task<decimal> TotalNetProfit(string email);
        Task<TaxLiabilityVM> TotalNetProfit1(string email);
        Task<TaxLiabilityVM> ReturnTaxLiability(string email);


    }
}