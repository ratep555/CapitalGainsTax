using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.ViewModels;

namespace Core.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> GetStockByIdAsync(int id);
        Task<IReadOnlyList<Stock>> GetStocksAsync();
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
        Task<IReadOnlyList<Country>> GetCountriesAsync();

        Task<IEnumerable<ClientPortfolioViewModel>> ShowClientPortfolio(QueryParameters queryParameters, string email );
        Task<IQueryable<ClientPortfolioViewModel>> ShowClientPortfolio1( string email);
        Task<IEnumerable<ClientPortfolioViewModel>> ShowClientPortfolio2(
           QueryParameters queryParameters,
           string email,
           string userId);
        
        string GetUserId();



    }
}


