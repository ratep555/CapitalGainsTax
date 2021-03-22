using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
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
    }
}









