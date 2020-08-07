using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mobile.Abstractions;
using Mobile.DTOs;
using System.Net.Http.Json;
namespace Mobile.Data
{
    public class ArticleService : IArticleService
    {
        private const string BaseUrl = "articles/";
        private readonly HttpClient _http;

        public ArticleService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Article> GetArticle(string barcode)
        {
            try
            {
                return await _http.GetFromJsonAsync<Article>($"{BaseUrl}LookupArticle/{barcode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
