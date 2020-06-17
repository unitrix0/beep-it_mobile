using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Data
{
    class DataService
    {
        private readonly HttpClient _client = new HttpClient();

        public async Task GetArticles()
        {
            var s = new string[0];
            var result = await _client.PostAsync("https://localhost:5001/login/", );
            
        }
    }
}
