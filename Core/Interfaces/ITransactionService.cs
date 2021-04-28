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

        string GetUserId();


    }
}