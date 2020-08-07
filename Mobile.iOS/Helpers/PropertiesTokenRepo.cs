using Mobile.Abstractions;
using Mobile.Data;
using Prism;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.iOS.Helpers
{
    class PropertiesTokenRepo : ITokenRepo
    {
        private readonly object _getLock = new object();
        
        public Task<string> GetRefreshTokenAsync()
        {
            lock (_getLock)
            {
                return Task<string>.Factory.StartNew(() => PrismApplicationBase.Current.Properties.ContainsKey("RefreshToken")
                        ? PrismApplicationBase.Current.Properties["RefreshToken"].ToString()
                        : "");
            }
        }

        public Task<string> GetIdentityTokenAsync()
        {
            lock (_getLock)
            {
                return Task<string>.Factory.StartNew(() => PrismApplicationBase.Current.Properties.ContainsKey(nameof(IdentityToken))
                    ? PrismApplicationBase.Current.Properties[nameof(IdentityToken)].ToString()
                    : "");
            }
        }

        public async Task SaveRefreshToken(string refreshToken)
        {
            PrismApplicationBase.Current.Properties[nameof(refreshToken)] = refreshToken;
            await PrismApplicationBase.Current.SavePropertiesAsync();
        }

        public async Task SaveIdentityToken(string identityToken)
        {
            PrismApplicationBase.Current.Properties[nameof(IdentityToken)] = identityToken;
            await PrismApplicationBase.Current.SavePropertiesAsync();
        }

        public async Task SavePermissionsToken(string permissionsToken)
        {
            PrismApplicationBase.Current.Properties["Permissions"] = permissionsToken;
            await PrismApplicationBase.Current.SavePropertiesAsync();
        }

        public Task<string> GetPermissionsTokenAsync()
        {
            return Task<string>.Factory.StartNew(() => PrismApplicationBase.Current.Properties.ContainsKey("Permissions")
                ? PrismApplicationBase.Current.Properties["Permissions"].ToString()
                : "");
        }

        public Task<string> GetUserObjectAsync()
        {
            return Task<string>.Factory.StartNew(() => PrismApplicationBase.Current.Properties.ContainsKey("MappedUser")
                ? PrismApplicationBase.Current.Properties["MappedUser"].ToString()
                : "");
        }

        public async Task SaveUserObject(string json)
        {
            PrismApplicationBase.Current.Properties["MappedUser"] = json;
            await PrismApplicationBase.Current.SavePropertiesAsync();
        }

        public void DeleteTokens()
        {
            PrismApplicationBase.Current.Properties.Remove(nameof(IdentityToken));
            PrismApplicationBase.Current.Properties.Remove("RefreshToken");
        }
    }
}