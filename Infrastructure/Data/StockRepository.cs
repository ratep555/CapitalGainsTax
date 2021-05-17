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

        public async Task<IEnumerable<ClientPortfolioViewModel>> ShowClientPortfolio(QueryParameters queryParameters,
         string email )
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

                                   }).AsEnumerable();  

                    if (queryParameters.HasQuery())
            {
            clientPortfolio = clientPortfolio
                    .Where(t => t.Symbol.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

           /*  foreach(var item in clientPortfolio)
            {
                var list1 = _context.StockTransactions.ToList();
                decimal basket = 0;
                decimal basket1 = 0;
                decimal basket3 = 0;
                foreach(var subitem in list1)
                {
                    if(subitem)
                }
            } */

            return await Task.FromResult(clientPortfolio.OrderBy(d => d.Symbol).GroupBy(d => d.Symbol).Select(d => d.FirstOrDefault()));

        }
        public async Task<IQueryable<ClientPortfolioViewModel>> ShowClientPortfolio1(string email )
        {
            IQueryable<ClientPortfolioViewModel> clientPortfolio = 
                                   (from t in _context.StockTransactions
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

                                   }).AsQueryable().OrderBy(d => d.Symbol);              

            return await Task.FromResult(clientPortfolio);

        }
        public async Task<IEnumerable<ClientPortfolioViewModel>> ShowClientPortfolio2(
        QueryParameters queryParameters,
         string email,
         string userId)
        {
            var clientPortfolio = await (from t in _context.StockTransactions
                                   where t.UserId == userId
                                   join s in _context.Stocks
                                   on t.StockId equals s.Id
                                   join u in _context.Users.Where(u => /* u.Id == userId && */ u.Email == email)                                    
                                   on t.UserId equals u.Id
                                   select new ClientPortfolioViewModel
                                   {
                                       StockId = s.Id,
                                       TransactionId = t.Id,
                                       UserId = userId,
                                       Symbol = s.Symbol,
                                       CurrentPrice = s.CurrentPrice,
                                       Email = u.Email,

                                       TotalQuantity = (_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == true).
                                       Sum(b => (int?)b.Quantity) ?? 0) - (_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.UserId == u.Id && b.Purchase == false).
                                       Sum(b => (int?)b.Quantity) ?? 0),
                                              
                                   }).ToListAsync();                    

            foreach(var item in clientPortfolio)
            {
                var list1 = _context.StockTransactions.ToList();
                decimal basket = 0;
                decimal basket1 = 0;
                decimal basket3 = 0;
                foreach(var subitem in list1)
                {
                    if(subitem.UserId == userId &&
                    subitem.StockId == item.StockId && subitem.Purchase == true)
                    {
                        if(subitem.Quantity != subitem.Resolved)
                        {
                            basket += (subitem.Quantity - subitem.Resolved);
                            basket1 += (subitem.Price * basket);

                            basket3 += (subitem.Quantity - subitem.Resolved);

                            item.AveragePriceOfPurchase = basket1 / basket3;                      
                        }
                        basket = 0;

                    }
                }
            } 
            return await Task.FromResult(clientPortfolio.OrderBy(d => d.Symbol).GroupBy(d => d.Symbol).Select(d => d.FirstOrDefault()));

        }
         public async Task<IEnumerable<ClientPortfolioViewModel>> ShowClientPortfolio3(
        //QueryParameters queryParameters,
         string email)
        {
            var clientPortfolio = await (from t in _context.StockTransactions
                                   where t.Email == email
                                   join s in _context.Stocks
                                   on t.StockId equals s.Id
                                   join u in _context.Users.Where(u => /* u.Id == userId && */ u.Email == email)                                    
                                   on t.Email equals u.Email
                                   select new ClientPortfolioViewModel
                                   {
                                       StockId = s.Id,
                                       TransactionId = t.Id,
                                       Symbol = s.Symbol,
                                       CurrentPrice = s.CurrentPrice,
                                       Email = email,

                                       TotalQuantity = (_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.Email == u.Email && b.Purchase == true).
                                       Sum(b => (int?)b.Quantity) ?? 0) - (_context.StockTransactions.
                                       Where(b => b.StockId == s.Id && b.Email == u.Email && b.Purchase == false).
                                       Sum(b => (int?)b.Quantity) ?? 0),
                                              
                                   }).ToListAsync();                    

            foreach(var item in clientPortfolio)
            {
                var list1 = _context.StockTransactions.ToList();
                decimal basket = 0;
                decimal basket1 = 0;
                decimal basket3 = 0;

                foreach(var subitem in list1)
                {
                    if(subitem.Email == email &&
                    subitem.StockId == item.StockId && subitem.Purchase == true)
                    {
                        if(subitem.Quantity != subitem.Resolved)
                        {
                            basket += (subitem.Quantity - subitem.Resolved);
                            basket1 += (subitem.Price * basket);

                            basket3 += (subitem.Quantity - subitem.Resolved);

                            item.AveragePriceOfPurchase = basket1 / basket3;                      
                        }
                        basket = 0;
                    }
                }                
            } 

            return await Task.FromResult(clientPortfolio.Where(d => d.TotalQuantity > 0).OrderBy(d => d.Symbol)
            .GroupBy(d => d.Symbol).Select(d => d.FirstOrDefault()));

        }

        public async Task<decimal> SumQuantityAndAveregePriceForAll(string email)
        {
            decimal basket = 0;
            decimal basket1 = 0;
            var list = new List<ClientPortfolioViewModel>();
            
            foreach (var item in list)
            {
                if(item.Email == email)
                {
                    basket = item.AveragePriceOfPurchase * item.TotalQuantity;
                    basket1 += basket;
                }
            }
            return await Task.FromResult(basket1);
        }
         public string GetUserId()
         {
            var userId = (from u in _context.Users
                         select u.Id).FirstOrDefault();         

            return userId;         
         } 

    }
}









