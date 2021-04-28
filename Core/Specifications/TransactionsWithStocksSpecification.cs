using Core.Entities;

namespace Core.Specifications
{
    public class TransactionsWithStocksSpecification : BaseSpecification<StockTransaction>
    {
        public TransactionsWithStocksSpecification(TransactionSpecParams transactionParams)
          : base(x =>
            (string.IsNullOrEmpty(transactionParams.Search) || x.Stock.Symbol.ToLower().Contains(transactionParams.Search))          
            &&
            (!transactionParams.StockId.HasValue || x.StockId == transactionParams.StockId) 
        )
        {
            AddInclude(x => x.Stock);
            AddOrderBy(x => x.Id);           
            ApplyPaging(transactionParams.PageSize * (transactionParams.PageIndex - 1), transactionParams.PageSize);

            if (!string.IsNullOrEmpty(transactionParams.Sort))
            {
                switch (transactionParams.Sort)
                {
                    case "symbolAsc":
                        AddOrderBy(p => p.Stock.Symbol);
                        break;
                    case "symbolDesc":
                        AddOrderByDescending(p => p.Stock.Symbol);
                        break;
                    default:
                        AddOrderBy(n => n.Id);
                        break;
                }
            }
        }

        public TransactionsWithStocksSpecification(int id) 
           : base(x => x.Id == id)
        {
            AddInclude(x => x.Stock);
        }
        public TransactionsWithStocksSpecification(string userId) : base(o => o.UserId == userId)
        {
                        AddInclude(x => x.Stock);
        }
    }
}




