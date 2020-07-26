using System.Threading.Tasks;
using Mobile.Abstractions;
using Mobile.Data;
using Prism;

namespace Mobile.iOS.Helpers
{
    class PropertiesTokenRepo : ITokenRepo
    {
        public Task<string> GetRefreshToken()
        {
            return Task<string>.Factory.StartNew(() => PrismApplicationBase.Current.Properties["RefreshToken"].ToString());
        }

        public Task<string> GetIdentityToken()
        {
            return Task<string>.Factory.StartNew(() => PrismApplicationBase.Current.Properties[nameof(IdentityToken)].ToString());
        }

        public Task SaveRefreshToken(string refreshToken)
        {
            return Task.Factory.StartNew(() =>
                {
                    PrismApplicationBase.Current.Properties[nameof(refreshToken)]= refreshToken;
                });
        }

        public Task SaveIdentityToken(string identityToken)
        {
            return Task.Factory.StartNew(() =>
            {
                PrismApplicationBase.Current.Properties[nameof(IdentityToken)] = identityToken;
            });
        }
    }
}