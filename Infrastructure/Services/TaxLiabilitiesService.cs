using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class TaxLiabilitiesService : ITaxLiabilitiesService
    {
        private readonly StoreContext _context;
        public TaxLiabilitiesService(StoreContext context)
        {
            _context = context;
        }

         public async Task<TaxLiability> FindTaxLiabilityById(int taxLiabilityId)
        {        
            return await _context.TaxLiabilities
            .Include(x => x.Surtax)
            .FirstOrDefaultAsync(x => x.Id == taxLiabilityId);
        }
         public async Task UpdateTaxLiability(TaxLiability taxLiability)
         {
            _context.Entry(taxLiability).State = EntityState.Modified;        
            await _context.SaveChangesAsync();
         }
        public async Task<decimal> TotalNetProfit(string email)
        { 
            decimal basket = 0;

            foreach (var item in _context.Stocks.ToList())
            {
                var totalNetProfit = (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == false)
                .Sum(t => t.Price * t.Quantity)) - 
                (_context.StockTransactions
                .Where(t => t.Email == email && t.StockId == item.Id && t.Purchase == true && t.Resolved > 0)
                .Sum(t => t.Resolved * t.Price));

                basket += totalNetProfit;
            }

            return await Task.FromResult(basket);
        }

        public async Task UpdateTaxLiability1(string email,
        TaxLiabilityToEditDTO taxLiabilityToEditDto)
        {     
            var user = await _context.AppUsers.Where(ap => ap.Email == email)
                       .FirstOrDefaultAsync();

            var taxLiability = await _context.TaxLiabilities
            .Include(t => t.Surtax)
            .Where(t => t.Email == email)
            .FirstOrDefaultAsync();

            var surtax = await _context.Surtaxes.Where(s => s.Id == user.SurtaxId)
                         .FirstOrDefaultAsync();
            
            decimal f = 100;
            decimal? basket4 = await TotalNetProfit(email);
            decimal? basket5 = (12 / f) * basket4;

            taxLiability.SurtaxId = surtax.Id;
            taxLiability.Year = DateTime.Now.Year;
            taxLiability.Email = email;    
            taxLiability.GrossProfit = basket4;

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.CapitalGainsTax = basket5;
            }
            else
            {
                taxLiability.CapitalGainsTax = 0;
            }        

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.SurtaxAmount = (surtax.Amount / f) * basket5;
            }
            else 
            {
                taxLiability.SurtaxAmount = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
            taxLiability.TotalTaxLiaility = basket5 + taxLiability.SurtaxAmount;
            }
            else 
            {
                taxLiability.TotalTaxLiaility = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
            taxLiability.NetProfit = basket4 - taxLiability.TotalTaxLiaility;
            }
            else 
            {
                 taxLiability.NetProfit = 0;
            }
            
            _context.Entry(taxLiability).State = EntityState.Modified;        
            await _context.SaveChangesAsync();         
        }
        
        public async Task UpdateTaxLiability2(string email,
        int id)
        {      
            var taxLiability = await _context.TaxLiabilities
            .Where(t => t.Email == email && t.Locked == false)
            .FirstOrDefaultAsync();

            var surtax = await _context.Surtaxes.Where(st => st.Id == id)
                         .FirstOrDefaultAsync();
            
            decimal f = 100;
            decimal? basket4 = await TotalNetProfit(email);
            decimal? basket5 = (12 / f) * basket4;

            taxLiability.SurtaxId = id;
            taxLiability.Year = DateTime.Now.Year;
            taxLiability.Email = email;    
            taxLiability.GrossProfit = basket4;

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.CapitalGainsTax = basket5;
            }
            else
            {
                taxLiability.CapitalGainsTax = 0;
            }        

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.SurtaxAmount = (surtax.Amount / f) * basket5;
            }
            else 
            {
                taxLiability.SurtaxAmount = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
            taxLiability.TotalTaxLiaility = basket5 + taxLiability.SurtaxAmount;
            }
            else 
            {
                taxLiability.TotalTaxLiaility = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
            taxLiability.NetProfit = basket4 - taxLiability.TotalTaxLiaility;
            }
            else 
            {
                 taxLiability.NetProfit = 0;
            }
            
            _context.Entry(taxLiability).State = EntityState.Modified;        
            await _context.SaveChangesAsync();         
        }
        
        public async Task UpdateUsersSurtaxId(string email, int surtaxId)
        {
            var user = await _context.AppUsers.Where(u => u.Email == email)
                       .FirstOrDefaultAsync();
            
            user.SurtaxId = surtaxId;
             
            _context.Entry(user).State = EntityState.Modified;        
            await _context.SaveChangesAsync(); 
        
        }

        public async Task<TaxLiability> FindTaxLiability(string email)
        {
            var taxLiability = await _context.TaxLiabilities
            .Include(x => x.Surtax)
            .Where(t => t.Email == email)
            .FirstOrDefaultAsync();

            return taxLiability;
        }
    
        public async Task<TaxLiability> UpdateNewTaxLiability(string email, int id)
        {
             var taxLiability = await _context.TaxLiabilities.Where(t => t.Email == email)
                                .Include (t => t.Surtax)
                                .FirstOrDefaultAsync();
            
             var surtax = await _context.Surtaxes.Where(s => s.Id == id).FirstOrDefaultAsync();

              
            decimal f = 100;
            decimal? basket4 = await TotalNetProfit(email);
            decimal? basket5 = (12 / f) * basket4;

            taxLiability.SurtaxId = id;
            taxLiability.Year = DateTime.Now.Year;
            taxLiability.Email = email;    
            taxLiability.GrossProfit = basket4;

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.CapitalGainsTax = basket5;
            }
            else
            {
                taxLiability.CapitalGainsTax = 0;
            }        

            if (basket4.HasValue && basket4 > 0)
            {
                taxLiability.SurtaxAmount = (surtax.Amount / f) * basket5;
            }
            else 
            {
                taxLiability.SurtaxAmount = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
            taxLiability.TotalTaxLiaility = basket5 + taxLiability.SurtaxAmount;
            }
            else 
            {
                taxLiability.TotalTaxLiaility = 0;
            }

            if (basket4.HasValue && basket4 > 0)
            {
            taxLiability.NetProfit = basket4 - taxLiability.TotalTaxLiaility;
            }
            else 
            {
                 taxLiability.NetProfit = 0;
            }
            
            _context.Entry(taxLiability).State = EntityState.Modified;        
            await _context.SaveChangesAsync();      

            return await Task.FromResult(taxLiability); 
        }

        public async Task<TaxLiability> FindTaxLiabilityByEmail(string email)
        {
            var taxliability = await _context.TaxLiabilities.Include(t => t.Surtax)
                               .Where(t => t.Email == email && t.Locked == false)
                               .FirstOrDefaultAsync();
            
            return await Task.FromResult(taxliability);
        }
    }
}











