using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StockRepository : IStockRepository
    {
        private readonly StoreContext _context;

        public StockRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IReadOnlyList<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Stock> GetStockByIdAsync(int id)
        {
            return await _context.Stocks
            .Include(p => p.Category)
            .Include(p => p.Country)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Stock>> GetStocksAsync()
        {
            return await _context.Stocks
            .Include(p => p.Category)
            .Include(p => p.Country)
            .ToListAsync();
        }

        public async Task<IEnumerable<ClientPortfolioViewModel>> ShowClientPortfolio(/* string userId, */ string email )
        {
            var clientPortfolio = (from t in _context.StockTransactions
                                   //where t.UserId == userId
                                   join s in _context.Stocks
                                   on t.StockId equals s.Id
                                   join u in _context.Users.Where(u => /* u.Id == userId && */ u.Email == email)                                    
                                   on t.UserId equals u.Id
                                   select new ClientPortfolioViewModel
                                   {
                                       StockId = s.Id,
                                       TransactionId = t.Id,
                                       UserId = u.Id,
                                       Symbol = s.Symbol,
                                       CurrentPrice = s.CurrentPrice,
                                       Email = u.Email,

                                       TotalQuantity = (_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == true).
                                       Sum(b => (int?)b.Quantity) ?? 0) - (_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == false).
                                       Sum(b => (int?)b.Quantity) ?? 0),

                                       TotalPriceOfPurchase = ((_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == true).
                                       Sum(b => (int?)b.Quantity) ?? 0) - (_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == false).
                                       Sum(b => (int?)b.Quantity) ?? 0)) * ((_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == true).
                                       Sum(b => b.Price * b.Quantity)) /
                                       (_context.StockTransactions.Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == true).
                                       Sum(b => b.Quantity))),

                                       AveragePriceOfPurchase = (((_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == true).
                                       Sum(b => (int?)b.Quantity) ?? 0) - (_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == false).
                                       Sum(b => (int?)b.Quantity) ?? 0)) * ((_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == true).
                                       Sum(b => b.Price * b.Quantity)) /
                                       (_context.StockTransactions.Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == true).
                                       Sum(b => b.Quantity)))) / ((_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == true).
                                       Sum(b => (int?)b.Quantity) ?? 0) - (_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == false).
                                       Sum(b => (int?)b.Quantity) ?? 0))


                                   }).AsEnumerable().OrderBy(d => d.Symbol);              

            return await Task.FromResult(clientPortfolio.GroupBy(d => d.Symbol).Select(d => d.FirstOrDefault()));

        }

    }
}









