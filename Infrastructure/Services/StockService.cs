using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly StoreContext _context;
        private readonly IGenericRepository<Stock> _stockRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IGenericRepository<Country> _countryRepo;
        private readonly IUnitOfWork _unitOfWork;

        public StockService(StoreContext context,
        IGenericRepository<Stock> stockRepo,
        IGenericRepository<Category> categoryRepo,
        IGenericRepository<Country> countryRepo,
        IUnitOfWork unitOfWork )
        {
            _context = context;
            _stockRepo = stockRepo;
            _categoryRepo = categoryRepo;
            _countryRepo = countryRepo;
            _unitOfWork = unitOfWork;
        }

    public async Task<IEnumerable<Stock>> ListAllStocksAsync()
    {
            return await _context.Stocks
            .Include(p => p.Category)
            .Include(p => p.Country)
            .ToListAsync();
    }

    public async Task<IEnumerable<Country>> ListAllCountriesAsync()
    {
            return await _unitOfWork.Repository<Country>().ListAllAsync();
    }
    public async Task<IEnumerable<Category>> ListAllCategoriesAsync()
    {
            return await _unitOfWork.Repository<Category>().ListAllAsync();
    }

    public async Task<int> TotalQuantity(string email, int stockId)
    {
        int totalQuantity = (_context.StockTransactions
        .Where(t => t.Email == email && t.StockId == stockId && t.Purchase == true)
        .Sum(t => t.Quantity)) -
        (_context.StockTransactions
        .Where(t => t.Email == email && t.StockId == stockId && t.Purchase == false)
        .Sum(t => t.Quantity));

        return await Task.FromResult(totalQuantity);
    }

     public async Task CreateStockAsync(Stock stock)
     {
             _context.Stocks.Add(stock);
             await _context.SaveChangesAsync();                    
     }
     public async Task CreateStockAsync1(Stock stock, string categoryName, string countryName)
     {
        int categoryId = _context.Categories.Where(c => c.CategoryName == categoryName).FirstOrDefaultAsync().Id;
        int countryId = _context.Countries.Where(c => c.CountryName == countryName).FirstOrDefaultAsync().Id;

        stock.CategoryId = categoryId;
        stock.CountryId = countryId;

        _stockRepo.Add(stock);
        await _unitOfWork.Complete();                    
     }
     public async Task<Stock> CreateStockAsync2(
     string symbol,
     decimal currentPrice,
     string companyName,
     string categoryName, 
     string countryName)
     {
        Stock stock = new Stock
        {
            Symbol = symbol,
            CurrentPrice = currentPrice,
            CompanyName = companyName,
            CategoryId =  _context.Categories.Where(c => c.CategoryName == categoryName).FirstOrDefaultAsync().Id,
            CountryId = _context.Countries.Where(c => c.CountryName == countryName).FirstOrDefaultAsync().Id
        };
       
        _context.Add(stock);
        await _context.SaveChangesAsync();        

        return stock;            
     }
     public async Task<int> ReturnCategoryId(string categoryName)
     {
         var categoryId = _context.Categories.Where(c => c.CategoryName == categoryName).FirstOrDefaultAsync().Id;
         return await Task.FromResult(categoryId);
     }
     public async Task<int> ReturnCountryId(string countryName)
     {
         var countryId = _context.Categories.Where(c => c.CategoryName == countryName).FirstOrDefaultAsync().Id;
         return await Task.FromResult(countryId);
     }

    public async Task<string> GetUserId()
    {
        var userId = await (from u in _context.Users
                            select u.Id).FirstOrDefaultAsync();

        return await Task.FromResult(userId);
    }
    public async Task<Stock> FindStockById(int stockId)
    {
        return await _context.Stocks.FindAsync(stockId);
    }
    public async Task SavingStock(Stock stock)
    {
        var stocky = _context.Stocks.Include(c => c.Category).Include(d => d.Country)
        .Where(s => s.Id == stock.Id).FirstOrDefault();

        _context.Add(stocky);
        await _context.SaveChangesAsync();    
    }
    public bool StockExists(int id)
    {
            return _context.Stocks.Any(e => e.Id == id);
    }
    public async Task UpdateStockAsync(Stock stock)
    {
            _context.Entry(stock).State = EntityState.Modified;        
            await _context.SaveChangesAsync();
    }
    public async Task<Stock> GetByIdAsync(int id)
    {
            return await _context.Stocks.FindAsync(id);
    }
    public async Task<List<Stock>> GetStocksAsync()
    {
            return await _context.Stocks
            .Include(p => p.Category)
            .Include(p => p.Country)
            .ToListAsync();
    }
    public async Task DeleteStockAsync(Stock stock)
    {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
    }

    
}
}







