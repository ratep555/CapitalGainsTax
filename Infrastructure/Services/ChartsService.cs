using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.ViewModels.Charts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class ChartsService : IChartsService
    {
        private readonly StoreContext _context;
        public ChartsService(StoreContext context)
        {
            _context = context;
        }

        public async Task<StockCategoryChartDto> ShowAllStocksWithCategories()
        {
            int agriculture = await _context.Stocks.Include(x => x.Category)
            .Where(x => x.Category.CategoryName == "Agriculture").CountAsync();

            int banking = await _context.Stocks.Include(x => x.Category)
            .Where(x => x.Category.CategoryName == "Banking").CountAsync();

            int construction = await _context.Stocks.Include(x => x.Category)
            .Where(x => x.Category.CategoryName == "Construction").CountAsync();

            int food = await _context.Stocks.Include(x => x.Category)
            .Where(x => x.Category.CategoryName == "Food").CountAsync();

            int shipping = await _context.Stocks.Include(x => x.Category)
            .Where(x => x.Category.CategoryName == "Shipping").CountAsync();

            int tourism = await _context.Stocks.Include(x => x.Category)
            .Where(x => x.Category.CategoryName == "Tourism").CountAsync();

            var list = new StockCategoryChartDto();
            list.Agriculture = agriculture;
            list.Banking = banking;
            list.Construction = construction;
            list.Food = food;
            list.Shipping = shipping;
            list.Tourism = tourism;

            return list;

        }
        public async Task<IEnumerable<AnnualProfitOrLoss>> ShowListOfProfitOrLosses(string email)
        {
            var list = await _context.AnnualProfitsOrLosses.Where(x => x.Email == email).ToListAsync();

            return list;
        } 
    }
}








