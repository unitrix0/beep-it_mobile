using System.Threading.Tasks;
using Mobile.DTOs;

namespace Mobile.Abstractions
{
    public interface ITokenRepo
    {
        Task<string> GetRefreshTokenAsync();
        Task<string> GetIdentityTokenAsync();
        Task SaveIdentityToken(string identityToken);
        Task SaveRefreshToken(string refreshToken);
        Task SavePermissionsToken(string permissionsToken);
        Task<string> GetPermissionsTokenAsync();
        Task<string> GetUserObjectAsync();
        Task SaveUserObject(string json);
        void DeleteTokens();
    }
}