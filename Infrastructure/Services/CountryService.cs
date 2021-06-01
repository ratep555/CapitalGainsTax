using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly IGenericRepository<Country> _countryRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CountryService(IGenericRepository<Country> countryRepo, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _countryRepo = countryRepo;
        }
        public async Task<IEnumerable<Country>> ListAllAsync()
        {
            return await _unitOfWork.Repository<Country>().ListAllAsync();
        }
        public async Task CreateCountryAsync(Country country)
        {
             _unitOfWork.Repository<Country>().Add(country);
              await _unitOfWork.Complete();                    
        }
        public async Task UpdateCountryAsync(Country country)
        {
                _countryRepo.Update(country);
                await _unitOfWork.Complete();
        }
        public async Task<Country> FindCountryByIdAsync(int id)
        {
            return await _countryRepo.GetByIdAsync(id);
        }
        public async Task DeleteCountry(Country country)
        {
                _countryRepo.Delete(country);
                await _unitOfWork.Complete();
        }


    }
}







