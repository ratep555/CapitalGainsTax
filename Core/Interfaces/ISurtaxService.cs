using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ISurtaxService
    {
            Task<IEnumerable<Surtax>> ListAllAsync();
            Task<IEnumerable<Surtax>> ListAllAsync1();

    }
}