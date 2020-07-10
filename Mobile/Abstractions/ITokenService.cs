using System.Threading.Tasks;
using Mobile.Data;
using Mobile.DTOs;

namespace Mobile.Abstractions
{
    public interface ITokenService
    {
        IdentityToken IdentityToken { get; }
        PermissionsToken PermissionsToken { get; }
        User UserInfos { get; }
        Task UpdatePermissionsToken();
    }
}