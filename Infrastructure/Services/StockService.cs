using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly StoreContext _context;
        public StockService(StoreContext context)
        {
            _context = context;
        }

         public async Task<int> TotalQuantity(string userId, int stockId)
        {
             int totalQuantity =  (_context.StockTransactions
             .Where(t => t.UserId == userId && t.StockId == stockId && t.Purchase == true)
             .Sum(t => t.Quantity)) - 
             (_context.StockTransactions
             .Where(t => t.UserId == userId && t.StockId == stockId && t.Purchase == false)
             .Sum(t => t.Quantity));

             return await Task.FromResult(totalQuantity);
        }

        public async Task<string> GetUserId()
        {
            var userId = await (from u in _context.Users
                         select u.Id).FirstOrDefaultAsync();         

            return await Task.FromResult(userId);         
        } 
    }
}