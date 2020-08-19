using Mobile.Abstractions;
using Mobile.DTOs;
using Mobile.Helpers;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Exception = System.Exception;

namespace Mobile.Data
{
    public class ArticleService : IArticleService
    {
        private const string BaseUrl = "articles";
        private readonly HttpClient _http;
        private readonly TokenContainer _tokens;

        public ArticlesBaseData BaseData { get; private set; }

        public ArticleService(HttpClient http, TokenContainer tokens)
        {
            _http = http;
            _tokens = tokens;
        }

        public async Task GetBaseData()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ArticlesBaseData>($"{BaseUrl}/GetBaseData/{_tokens.Permissions.EnvironmentId}");
                BaseData = response;
            }
            catch (HttpResponseException responseException)
            {
                Console.WriteLine($"{responseException.Message} - {await responseException.Response.Content.ReadAsStringAsync()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<Article> GetArticle(string barcode)
        {
            try
            {
                return await _http.GetFromJsonAsync<Article>($"{BaseUrl}/LookupArticle/{barcode}");
            }
            catch (HttpResponseException responseException)
            {
                Console.WriteLine($"{responseException.Message} - {await responseException.Response.Content.ReadAsStringAsync()}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
