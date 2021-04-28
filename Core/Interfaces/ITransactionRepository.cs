using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<StockTransaction> GetTransactionByIdAsync(int id);
        Task<IReadOnlyList<StockTransaction>> GetTransactionsAsync();
    }
}