using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class FifoExtension
    {
    private static decimal SumOfLastTransaction(IEnumerable<StockTransaction> stockTransactions, int max)
    {
        decimal result = 0;
        int sum = 0;
        foreach(var stockTransaction in stockTransactions.OrderByDescending(x => x.Id))
        {
            
            if(sum + stockTransaction.Quantity <= max)
            {
                result += stockTransaction.Quantity * stockTransaction.Price;
                sum += stockTransaction.Quantity;
            }
            else
            {
                result += (max - sum) * stockTransaction.Price;
                return result;
            }
        }
        return result;
    }
    }
}