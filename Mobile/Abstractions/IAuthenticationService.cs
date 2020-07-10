using System.Threading.Tasks;

namespace Mobile.Abstractions
{
    public interface IAuthenticationService
    {
        Task Login(string userName, string password);
    }
}