using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly StoreContext _context;
        public TransactionRepository(StoreContext context)
        {
            _context = context;

        }
        public async Task<StockTransaction> GetTransactionByIdAsync(int id)
        {
              return await _context.StockTransactions.
              Include(p => p.Stock).
              FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<IReadOnlyList<StockTransaction>> GetTransactionsAsync()
        {
             return await _context.StockTransactions
            .Include(p => p.Stock)
            .ToListAsync();        }
    }
}