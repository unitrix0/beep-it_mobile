using Mobile.Abstractions;
using Mobile.DTOs;
using Mobile.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mobile.Data
{
    public delegate Task<RefreshTokenResponse> RefreshTokenDelegate();

    public class AuthenticationService : IAuthenticationService
    {
        private const string BaseUrl = "auth/";
        private readonly TokenContainer _tokenContainer;
        private readonly HttpClient _client;
        
        public AuthenticationService(HttpClient client, TokenContainer tokenContainer)
        {
            _client = client;
            _tokenContainer = tokenContainer;
        }

        public async Task Login(string userName, string password)
        {
            var response = await _client.PostAsync<LoginResponse>($"{BaseUrl}/login", new
            {
                userName,
                password,
                cameras = new string[0]
            });

            await _tokenContainer.LoadLoginResponse(response);
        }

        public async Task UpdatePermissionsTokenAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<RefreshTokenResponse> RefreshTokenAsync()
        {
            var response = await _client.PostAsync<RefreshTokenResponse>($"{BaseUrl}/refreshToken", new
            {
                token = await _tokenContainer.GetIdentityTokenAsync(),
                refershToken = await _tokenContainer.GetRefreshToken()
            });

            return response;
        }
    }
}
