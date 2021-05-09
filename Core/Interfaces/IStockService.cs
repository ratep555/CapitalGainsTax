using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStockService
    {
        Task<int> TotalQuantity(string userId, int stockId);
        Task<string> GetUserId();


    }
}