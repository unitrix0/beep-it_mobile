using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace Mobile.Helpers
{
    static class HttpClientExtensions
    {
        private const string JsonMediaType = "application/json";

        public static async Task<string> GetStringByQueryAsync(this HttpClient client, string requestUri, object paramObj)
        {
            var qry = new QueryBuilder(paramObj.ToKeyValuePairs());
            return await client.GetStringAsync(requestUri + qry);
        }

        public static async Task<HttpResponseMessage> GetAsyncQuery(this HttpClient client, string requestUri,
            object paramObj)
        {
            var qry = new QueryBuilder(paramObj.ToKeyValuePairs());
            return await client.GetAsync(requestUri + qry);
        }

        public static async Task<HttpResponseMessage> DeleteAsyncQuery(this HttpClient client, string requestUri,
            object paramObj)
        {
            var qry = new QueryBuilder(paramObj.ToKeyValuePairs());
            return await client.DeleteAsync(requestUri + qry);
        }

        public static async Task<T> PostAsync<T>(this HttpClient client, string requestUri, object value)
        {
            string json = JsonConvert.SerializeObject(value);
            var content = new StringContent(json, Encoding.UTF8, JsonMediaType);

            HttpResponseMessage result = await client.PostAsync(requestUri, content);
            string resultContent = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode) return JsonConvert.DeserializeObject<T>(resultContent); 

            if (result.StatusCode == HttpStatusCode.InternalServerError &&
                result.Headers.Contains("Application-Error"))
            {
                var appError = result.Headers
                    .First(h => h.Key == "Application-Error")
                    .Value.First();
                var exception = JsonConvert.DeserializeObject<Exception>(resultContent, new JsonSerializerSettings()
                {
                    StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                });
                throw new Exception($"{(int)result.StatusCode} - {result.StatusCode} \n{appError}", exception);
            }

            throw new Exception($"{(int)result.StatusCode} - {result.StatusCode}");

        }

        private static Dictionary<string, string> ToKeyValuePairs(this object source)
        {
            Dictionary<string, string> valuePairs = source.GetType().GetProperties()
                .Select(p => new { p.Name, value = p.GetValue(source).ToString() })
                .ToDictionary(x => x.Name, x => x.value);

            return valuePairs;
        }
    }
}
