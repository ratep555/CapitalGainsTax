using Core.Entities;

namespace Core.Specifications
{
    public class TransactionWithFiltersForCountSpecification : BaseSpecification<StockTransaction>
    {
        public TransactionWithFiltersForCountSpecification(TransactionSpecParams transactionParams)
         : base(x => 
                (string.IsNullOrEmpty(transactionParams.Search) || x.Stock.Symbol.ToLower().Contains(transactionParams.Search)) &&
                (!transactionParams.StockId.HasValue || x.StockId == transactionParams.StockId) 
            )
        {
            
        }
    }
}