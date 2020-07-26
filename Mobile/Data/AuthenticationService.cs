using Mobile.Abstractions;
using Mobile.DTOs;
using Mobile.Helpers;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mobile.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly TokenContainer _tokenContainer;
        private readonly HttpClient _client = new HttpClient();

        public AuthenticationService(TokenContainer tokenContainer)
        {
            _tokenContainer = tokenContainer;
            _client.BaseAddress = new Uri("http://drone02.hive.loc:5000/api/");
        }

        public async Task Login(string userName, string password)
        {
            var token = await _tokenContainer.GetIdentityToken();
            
            var response = await _client.PostAsync<LoginResponse>("auth/login", new
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

        public async Task RefershTokenAsync()
        {
            throw new NotImplementedException();
        }
    }
}
