using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.ViewModels.Charts;

namespace Core.Interfaces
{
    public interface IChartsService
    {
        Task<StockCategoryChartDto> ShowAllStocksWithCategories();
        Task<IEnumerable<AnnualProfitOrLoss>> ShowListOfProfitOrLosses(string email);

    }
}