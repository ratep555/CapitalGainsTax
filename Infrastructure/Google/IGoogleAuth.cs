using System.Threading.Tasks;
using Core.ViewModels;
using Google.Apis.Auth;

namespace Infrastructure.Google
{
    public interface IGoogleAuth
    {
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthDto externalAuth);

    }
}