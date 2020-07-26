using System.Threading.Tasks;
using Mobile.Abstractions;
using Mobile.Data;
using Mobile.DTOs;

namespace Mobile.Helpers
{
    public class TokenContainer
    {
        private readonly ITokenRepo _tokenRepo;

        public User User { get; protected set; }
        public PermissionsToken Permissions { get; protected set;}
        public IdentityToken IdentityToken { get; protected set;}

        public TokenContainer(ITokenRepo tokenRepo)
        {
            _tokenRepo = tokenRepo;
        }

        public async Task LoadLoginResponse(LoginResponse loginResponse)
        {
            IdentityToken = new IdentityToken(loginResponse.IdentityToken);
            Permissions = new PermissionsToken(loginResponse.PermissionsToken);
            User = loginResponse.MappedUser;
            await _tokenRepo.SaveIdentityToken(loginResponse.IdentityToken);
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
    }
}