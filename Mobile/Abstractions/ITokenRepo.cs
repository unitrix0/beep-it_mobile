using System.Threading.Tasks;
using Mobile.DTOs;

namespace Mobile.Abstractions
{
    public interface ITokenRepo
    {
        Task<string> GetRefreshToken();
        Task<string> GetIdentityToken();
        Task SaveIdentityToken(string identityToken);
        Task SaveRefreshToken(string refreshToken);
        Task SavePermissionsToken(string permissionsToken);
        Task<string> GetPermissionsToken();
        Task<string> GetUserObject();
        Task SaveUserObject(string json);
    }
}