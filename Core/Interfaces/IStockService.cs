using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IStockService
    {
        Task<int> TotalQuantity(string email, int stockId);
        Task<string> GetUserId();
        Task<IEnumerable<Stock>> ListAllStocksAsync();
        Task<IEnumerable<Stock>> ListAllStocksAsync1(QueryParameters queryParameters);

        Task<IEnumerable<Country>> ListAllCountriesAsync();
        Task<IEnumerable<Category>> ListAllCategoriesAsync();  
        Task CreateStockAsync(Stock stock);
        Task<int> ReturnCategoryId(string categoryName);
        Task<int> ReturnCountryId(string countryName);
        Task CreateStockAsync1(Stock stock, string categoryName, string countryName);
        Task<Stock> FindStockById(int stockId);
        Task SavingStock(Stock stock);
        bool StockExists(int id);
        Task UpdateStockAsync(Stock stock);
        Task<Stock> GetByIdAsync(int id);
        Task<List<Stock>> GetStocksAsync();
        Task DeleteStockAsync(Stock stock);

        
        Task<Stock> CreateStockAsync2(
     string symbol,
     decimal currentPrice,
     string companyName,
     string categoryId, 
     string countryId);

    Task<List<Stock1>> LoadExcelFile2(FileInfo file);
    Task Excelica(FileInfo file);

    }      
    

}

     














