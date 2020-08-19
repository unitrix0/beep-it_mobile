using Mobile.DTOs;
using Mobile.Helpers;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mobile
{
    public class ErrorResponseHandler : DelegatingHandler
    {
        private readonly TokenContainer _tokenContainer;
        private bool _refreshingToken;
        private Task<RefreshTokenResponse> _refreshTask;

        public ErrorResponseHandler(TokenContainer tokenContainer)
        {
            _tokenContainer = tokenContainer;
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode) return response;

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized
                    when _tokenContainer.IdentityToken.IsExpired:
                    return await RefreshIdentityToken(request, cancellationToken);
                default:
                    throw new HttpResponseException(response);
            }
        }

        private async Task<HttpResponseMessage> RefreshIdentityToken(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!_refreshingToken)
            {
                Console.WriteLine("************** Refreshing token");
                _refreshingToken = true;
                _refreshTask = GetNewTokenAsync(cancellationToken);
                RefreshTokenResponse response = await _refreshTask;
                _refreshingToken = false;

                await _tokenContainer.LoadNewToken(response);
                request.Headers.UpdateBearerToken(response.IdentityToken);
                return await base.SendAsync(request, cancellationToken);
            }
            else
            {
                Console.WriteLine("************** Waiting for token refresh");
                var response = await _refreshTask;
                request.Headers.UpdateBearerToken(response.IdentityToken);
                return await base.SendAsync(request, cancellationToken);
            }
        }

        private async Task<RefreshTokenResponse> GetNewTokenAsync(CancellationToken cancellationToken)
        {
            //TODO URI
            string identityToken = await _tokenContainer.GetIdentityTokenAsync();
            string refreshToken = await _tokenContainer.GetRefreshToken();
            string json = JsonConvert.SerializeObject(new
            {
                identityToken,
                refreshToken
            });

            var req = new HttpRequestMessage(HttpMethod.Post, "http://drone02.hive.loc:5000/api/auth/refreshToken")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await base.SendAsync(req, cancellationToken);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<RefreshTokenResponse>(await response.Content.ReadAsStringAsync());

            await _tokenContainer.Logout();
            throw new HttpResponseException(response);
        }
    }
}