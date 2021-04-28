using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.ViewModels;

namespace Core.Specifications
{
    public class StocksWithCategoriesAndCountriesSpecification : BaseSpecification<Stock>
    {
        public StocksWithCategoriesAndCountriesSpecification(StockSpecParams stockParams)
        : base(x =>
            (string.IsNullOrEmpty(stockParams.Search) || x.Symbol.ToLower().Contains(stockParams.Search))          
            &&
            (!stockParams.CategoryId.HasValue || x.CategoryId == stockParams.CategoryId) &&
            (!stockParams.CountryId.HasValue || x.CountryId == stockParams.CountryId)
        )
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Country);
            AddOrderBy(x => x.Symbol);
            ApplyPaging(stockParams.PageSize * (stockParams.PageIndex - 1), stockParams.PageSize);

            if(!string.IsNullOrEmpty(stockParams.Sort)) 
            {
                switch (stockParams.Sort)
                {
                    case "categoryAsc":
                        AddOrderBy(p => p.Category);
                        break;
                    case "categoryDesc":
                        AddOrderByDescending(p => p.Category);
                        break;
                    default:
                        AddOrderBy(s => s.Symbol);
                        break;
                }
            }
        }

        public StocksWithCategoriesAndCountriesSpecification(int id) 
           : base(x => x.Id == id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Country);
        }
    }
}