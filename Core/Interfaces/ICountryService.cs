using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> ListAllAsync();
        Task<IEnumerable<Country>> ListAllAsync1(QueryParameters queryParameters);

        Task CreateCountryAsync(Country country);
        Task UpdateCountryAsync(Country country);
        Task<Country> FindCountryByIdAsync(int id);
        Task DeleteCountry(Country country);



    }
}