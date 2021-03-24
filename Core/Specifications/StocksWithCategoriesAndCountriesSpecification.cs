using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class StocksWithCategoriesAndCountriesSpecification : BaseSpecification<Stock>
    {
        public StocksWithCategoriesAndCountriesSpecification()
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Country);
        }

        public StocksWithCategoriesAndCountriesSpecification(int id) 
           : base(x => x.Id == id)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Country);
        }
    }
}