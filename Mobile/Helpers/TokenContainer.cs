using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Mobile.Abstractions;
using Mobile.Data;
using Mobile.DTOs;
using Mobile.Views;
using Newtonsoft.Json;
using Prism.Navigation;

namespace Mobile.Helpers
{
    public class TokenContainer
    {
        private readonly ITokenRepo _tokenRepo;
        private readonly INavigationService _navService;

        public User User { get; private set; }
        public PermissionsToken Permissions { get; private set;}
        public IdentityToken IdentityToken { get; private set;}
        
        public TokenContainer(ITokenRepo tokenRepo, INavigationService navService)
        {
            _tokenRepo = tokenRepo;
            _navService = navService;
        }

        public async Task LoadFromRepoAsync()
        {
            User = await GetUserObjectAsync();
            Permissions = new PermissionsToken(await _tokenRepo.GetPermissionsTokenAsync());
            IdentityToken = new IdentityToken(await GetIdentityTokenAsync());
        }

        public async Task LoadLoginResponse(LoginResponse loginResponse)
        {
            IdentityToken = new IdentityToken(loginResponse.IdentityToken);
            Permissions = new PermissionsToken(loginResponse.PermissionsToken);
            User = loginResponse.MappedUser;

            await SaveUserObject(User);
            await _tokenRepo.SaveIdentityToken(loginResponse.IdentityToken);
            await _tokenRepo.SavePermissionsToken(loginResponse.PermissionsToken);
            await _tokenRepo.SaveRefreshToken(loginResponse.RefreshToken);
        }

        public async Task LoadNewToken(RefreshTokenResponse response)
        {
            IdentityToken = new IdentityToken(response.IdentityToken);
            await _tokenRepo.SaveIdentityToken(response.IdentityToken);
            await _tokenRepo.SaveRefreshToken(response.RefreshToken);
        }

        public async Task<string> GetRefreshToken()
        {
            return await _tokenRepo.GetRefreshTokenAsync();
        }

        public async Task<string> GetIdentityTokenAsync()
        {
            return await _tokenRepo.GetIdentityTokenAsync();
        }

        private async Task<User> GetUserObjectAsync()
        {
            string json = await _tokenRepo.GetUserObjectAsync();
            return json == null ? new User() : JsonConvert.DeserializeObject<User>(json);
        }

        private async Task SaveUserObject(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            await _tokenRepo.SaveUserObject(json);
        }

        public async Task Logout()
        {
            _tokenRepo.DeleteTokens();
            IdentityToken= new IdentityToken(string.Empty);
            var r =await _navService.NavigateAbsolutAsync($"{nameof(LoginPage)}");
        }
    }
}