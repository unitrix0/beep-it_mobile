using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mobile.Abstractions;
using Mobile.DTOs;
using Mobile.Helpers;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Mobile.Data
{
    public class AuthenticationService : IAuthenticationService, ITokenService
    {
        private readonly HttpClient _client = new HttpClient();

        public IdentityToken IdentityToken { get; private set; }
        public PermissionsToken PermissionsToken{ get; private set; }
        public User UserInfos { get; private set; }


        public AuthenticationService()
        {
            _client.BaseAddress = new Uri("http://drone02.hive.loc:5000/api/");
        }

        public async Task Login(string userName, string password)
        {
            var token = await _client.PostAsync<LoginToken>("auth/login", new
            {
                userName,
                password,
                cameras = new string[0]
            });

            IdentityToken = new IdentityToken(token.IdentityToken);
            PermissionsToken = new PermissionsToken(token.PermissionsToken);
            UserInfos = token.MappedUser;
        }

        public async Task UpdatePermissionsToken()
        {
            throw new NotImplementedException();
        }
    }
}
