using Mobile.Abstractions;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mobile.Droid.Helpers
{
    public class SecureTokenRepo : ITokenRepo
    {
        public async Task<string> GetRefreshTokenAsync()
        {
            return await SecureStorage.GetAsync("RefreshToken");
        }

        public async Task<string> GetIdentityTokenAsync()
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

        public async Task SavePermissionsToken(string permissionsToken)
        {
            await SecureStorage.SetAsync("Permissions", permissionsToken);
        }

        public async Task<string> GetPermissionsTokenAsync()
        {
            return await SecureStorage.GetAsync("Permissions");
        }

        public async Task<string> GetUserObjectAsync()
        {
            return await SecureStorage.GetAsync("MappedUser");
        }

        public async Task SaveUserObject(string json)
        {
            await SecureStorage.SetAsync("MappedUser", json);
        }

        public void DeleteTokens()
        {
            SecureStorage.Remove("IdentityToken");
            SecureStorage.Remove("RefreshToken");
        }
    }
}