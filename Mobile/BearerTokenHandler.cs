using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mobile.Helpers;

namespace Mobile
{
    /// <summary>
    /// Fügt dem Request den Bearer Token hinzu
    /// </summary>
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly TokenContainer _tokenContainer;

        public BearerTokenHandler(TokenContainer tokenContainer, ErrorResponseHandler innerHandler)
        {
            _tokenContainer = tokenContainer;
            InnerHandler = innerHandler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string identityToken = await _tokenContainer.GetIdentityTokenAsync();
            request.Headers.UpdateBearerToken(identityToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
