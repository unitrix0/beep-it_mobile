using System.Threading.Tasks;
using Mobile.Abstractions;
using Xamarin.Essentials;

namespace Mobile.Droid.Helpers
{
    public class SecureTokenRepo : ITokenRepo
    {
        public async Task<string> GetRefreshToken()
        {
            return await SecureStorage.GetAsync("RefreshToken");
        }

        public async Task<string> GetIdentityToken()
        {
            return await SecureStorage.GetAsync("IdentityToken");
        }

        public async Task SaveIdentityToken(string identityToken)
        {
            await SecureStorage.SetAsync("IdentityToken", identityToken);
        }

        public async Task SaveRefreshToken(string refreshToken)
        {
            await SecureStorage.SetAsync("RefreshToken", refreshToken);
        }
    }
}