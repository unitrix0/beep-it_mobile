using System.Threading.Tasks;

namespace Mobile.Abstractions
{
    public interface ITokenRepo
    {
        Task<string> GetRefreshToken();
        Task<string> GetIdentityToken();
        Task SaveIdentityToken(string identityToken);
        Task SaveRefreshToken(string refreshToken);
    }
}