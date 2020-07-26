using System.Threading.Tasks;
using Mobile.Data;
using Mobile.DTOs;

namespace Mobile.Abstractions
{
    public interface ITokenService
    {
        Task UpdatePermissionsTokenAsync();
        Task RefershTokenAsync();
    }
}