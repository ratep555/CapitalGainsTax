using System.Threading.Tasks;
using Core.Entities;
using Core.ViewModels;

namespace Core.Interfaces
{
    public interface ITokenService
    {
         string CreateToken(AppUser user);

    }
}