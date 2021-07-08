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
       Task<TransactionsForUserListVM> ShowTransactionsForSpecificUser5(QueryParameters queryParameters,
         string email);
        Task InitializeTaxLiability(string email);
        Task UpdateTaxLiability(string email);


        Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser(string email);
        Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser1(QueryParameters queryParameters, string email);
        Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser2(QueryParameters queryParameters, string email);
        Task<IEnumerable<StockTransaction>> Fifo(string email);
        Task<IEnumerable<StockTransaction>> Again(string email);
        Task<IEnumerable<StockTransaction>> Fifo5(string email);

        Task<TaxLiabilityVM> ReturnTaxLiability7(string userId);



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
        Task<IEnumerable<TransactionsForUserVM>> ShowTransactionsForSpecificUser4(
        QueryParameters queryParameters,
        string email);
        Task<string> GetUserId();
        Task<int> TotalQuantity(string userId, int stockId);
        Task<decimal> TotalNetProfit(string email);
        Task<TaxLiabilityVM> TotalNetProfit1(string email);
        Task<TaxLiabilityVM> ReturnTaxLiability(string email);
        Task<TaxLiabilityVM> ReturnTaxLiability1(string email, int surtaxId);

        Task<StockTransaction> GetTransactionByEmailAndId(string email, int stockId, int quantity);
        Task<Stock> FindStockById(int id);

        Task<string> GetUserId7(string email);





    }
}