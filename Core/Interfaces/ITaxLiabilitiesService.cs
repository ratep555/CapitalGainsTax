using System.Threading.Tasks;
using Core.Entities;
using Core.ViewModels;

namespace Core.Interfaces
{
    public interface ITaxLiabilitiesService
    {
        Task<TaxLiability> FindTaxLiabilityById(int taxLiabilityId);
        Task UpdateTaxLiability(TaxLiability taxLiability);
        Task UpdateTaxLiability1(string email,
        TaxLiabilityToEditDTO taxLiabilityToEditDto);
        Task<decimal> TotalNetProfit(string email);
        Task UpdateUsersSurtaxId(string email, int surtaxId);
        Task<TaxLiability> FindTaxLiability(string email);

        Task<TaxLiability> UpdateNewTaxLiability(string email, int id);
         Task UpdateTaxLiability2(string email,
        int id);
        Task<TaxLiability> FindTaxLiabilityByEmail(string email);

        Task<FinalTaxLiabilityVM> FindAnnualByEmail(string email, int id);

        public decimal Broj();
        public decimal Broj1();
        public decimal Broj2();
        Task<AnnualProfitOrLoss> GiveMeAnnual(string email);





    }
    
}