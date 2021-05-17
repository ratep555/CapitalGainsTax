using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStockService
    {
        Task<int> TotalQuantity(string email, int stockId);
        Task<string> GetUserId();


    }
}