using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISecurityService
    {
        Task<List<Stock>> GetMeStocks(string symbol, decimal price);
        Task GetMeStocks1(string symbol, decimal price);


    }
}