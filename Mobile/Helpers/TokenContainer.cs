using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Mobile.Abstractions;
using Mobile.Data;
using Mobile.DTOs;
using Newtonsoft.Json;

namespace Mobile.Helpers
{
    public class TokenContainer
    {
        private readonly ITokenRepo _tokenRepo;

        public User User { get; private set; }
        public PermissionsToken Permissions { get; private set;}
        public IdentityToken IdentityToken { get; private set;}

        public TokenContainer(ITokenRepo tokenRepo)
        {
            _tokenRepo = tokenRepo;
        }

        public async Task LoadFromRepo()
        {
            User = await GetUserObject();
            Permissions = new PermissionsToken(await GetPermissionsToken());
            IdentityToken = new IdentityToken(await GetIdentityToken());
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
            return await _tokenRepo.GetRefreshToken();
        }

        public async Task<string> GetIdentityToken()
        {
            return await _tokenRepo.GetIdentityToken();
        }

        private async Task<string> GetPermissionsToken()
        {
            return await _tokenRepo.GetPermissionsToken();
        }

        private async Task<User> GetUserObject()
        {
            string json = await _tokenRepo.GetUserObject();
            return json == null ? new User() : JsonConvert.DeserializeObject<User>(json);
        }

        private async Task SaveUserObject(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            await _tokenRepo.SaveUserObject(json);
        }
    }
}