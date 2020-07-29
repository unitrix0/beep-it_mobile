using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ImTools;
using Mobile.Abstractions;
using Mobile.Helpers;

namespace Mobile
{
    public class UnauthorizedResponseHandler : DelegatingHandler
    {
        private readonly TokenContainer _tokenContainer;
        private readonly IAuthenticationService _authService;
        private bool _refreshingToken;

        private event EventHandler RefreshDone;

        public UnauthorizedResponseHandler(TokenContainer tokenContainer, IAuthenticationService authService)
        {
            _tokenContainer = tokenContainer;
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.UpdateBearerToken(await _tokenContainer.GetIdentityToken());
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized
                    when _tokenContainer.IdentityToken.IsExpired:
                    return await RefreshIdentityToken(request, cancellationToken);
                case HttpStatusCode.Unauthorized:
                    throw new Exception(response.ReasonPhrase);
                default:
                    return response;
            }
        }

        private async Task<HttpResponseMessage> RefreshIdentityToken(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!_refreshingToken)
            {
                _refreshingToken = true;
                await _authService.RefershTokenAsync();
                _refreshingToken = false;
                RefreshDone?.Invoke(this, EventArgs.Empty);

                request.Headers.UpdateBearerToken(await _tokenContainer.GetIdentityToken());
                return await base.SendAsync(request, cancellationToken);
            }

            return await RefreshDone.To(async handler =>
            {
                request.Headers.UpdateBearerToken(await _tokenContainer.GetIdentityToken());
                return await base.SendAsync(request, cancellationToken);
            });
        }
    }
}