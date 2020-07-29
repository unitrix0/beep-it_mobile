//using JWT;
//using JWT.Serializers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mobile.Data
{
    public class IdentityToken
    {
        private readonly int _userId;
        private readonly DateTime _expiryDate;

        public int UserId => _userId;
        public string UserName { get; }
        public string[] Roles { get; }
        public bool IsExpired => IsVaid && _expiryDate < DateTime.UtcNow;
        public bool IsVaid { get; private set; }

        public IdentityToken(string tokenString)
        {
            if (string.IsNullOrEmpty(tokenString))
            {
                UserName = "";
                Roles = new string[0];
                IsVaid = false;
            }

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadJwtToken(tokenString);
            IsVaid = token != null;

            int.TryParse(token?.Claims.Single(c => c.Type == JwtRegisteredClaimNames.NameId).Value, out _userId);
            UserName = token?.Claims.Single(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value;
            Roles = token?.Claims.Where(c => c.Type == "role")
                .Select(c => c.Value).ToArray();

            long expiryDateUnix = long.Parse(token?.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Exp).Value ?? "0");
            _expiryDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);
        }
    }
}