using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class SurtaxService : ISurtaxService
    {
        private readonly StoreContext _context;

        public SurtaxService(StoreContext context)
        {
            _context = context;
        }

         public async Task<IEnumerable<Surtax>> ListAllAsync()
        {
            return await _context.Surtaxes.ToListAsync();
        }
        public async Task<IEnumerable<Surtax>> ListAllAsync1()
        {
            return await _context.Surtaxes.OrderBy(s => s.Residence).ToListAsync();
        }
        public async Task<Surtax> FindSurtaxById(int id) 
        {
            var surtax = await _context.Surtaxes.Where(s => s.Id == id).FirstOrDefaultAsync();           
            return surtax;
        }
    }
}









