using System.Threading.Tasks;
using Mobile.Data;
using Mobile.DTOs;

namespace Mobile.Abstractions
{
    public interface IAuthenticationService
    {
        Task Login(string userName, string password);
    }
}