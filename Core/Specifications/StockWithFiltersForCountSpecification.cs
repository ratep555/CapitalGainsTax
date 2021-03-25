using Core.Entities;

namespace Core.Specifications
{
    public class StockWithFiltersForCountSpecification : BaseSpecification<Stock>
    {
        public StockWithFiltersForCountSpecification(StockSpecParams stockParams)
         : base(x =>
            (string.IsNullOrEmpty(stockParams.Search) || x.Symbol.ToLower().Contains(stockParams.Search))
            &&
            (!stockParams.CategoryId.HasValue || x.CategoryId == stockParams.CategoryId) &&
            (!stockParams.CountryId.HasValue || x.CountryId == stockParams.CountryId))
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Country);
        }
    }
}