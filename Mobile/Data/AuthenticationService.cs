using Mobile.Abstractions;
using Mobile.DTOs;
using Mobile.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mobile.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string BaseUrl = "auth";
        private readonly TokenContainer _tokenContainer;
        private readonly IArticleService _articleService;
        private readonly HttpClient _client;
        
        public AuthenticationService(HttpClient client, TokenContainer tokenContainer, IArticleService articleService)
        {
            _client = client;
            _tokenContainer = tokenContainer;
            _articleService = articleService;
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
            await _articleService.GetBaseData();
            Console.WriteLine($"------------------ New Token Expires {_tokenContainer.IdentityToken.ExpireDate.ToLongTimeString()}");
        }
    }
}
