using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
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

        public static IObservable<string> GetStringByQueryAsync(this HttpClient client, string requestUri, object paramObj)
        {
            return Observable.Create<string>(async observer =>
            {
                try
                {
                    var qry = new QueryBuilder(paramObj.ToKeyValuePairs());
                    string result = await client.GetStringAsync(requestUri + qry);

                    observer.OnNext(result);
                    observer.OnCompleted();
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
            });

        }

        public static IObservable<HttpResponseMessage> GetAsyncQuery(this HttpClient client, string requestUri,
            object paramObj)
        {
            return Observable.Create<HttpResponseMessage>(async observer =>
            {
                try
                {
                    var qry = new QueryBuilder(paramObj.ToKeyValuePairs());
                    HttpResponseMessage response = await client.GetAsync(requestUri + qry);

                    observer.OnNext(response);
                    observer.OnCompleted();
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
            });
        }

        public static IObservable<HttpResponseMessage> DeleteAsyncQuery(this HttpClient client, string requestUri,
            object paramObj)
        {
            return Observable.Create<HttpResponseMessage>(async observer =>
            {
                try
                {
                    var qry = new QueryBuilder(paramObj.ToKeyValuePairs());
                    HttpResponseMessage result = await client.DeleteAsync(requestUri + qry);

                    observer.OnNext(result);
                    observer.OnCompleted();
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
            });

        }

        public static IObservable<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, object value)
        {
            return Observable.Create<HttpResponseMessage>(async observer =>
            {
                try
                {
                    string json = JsonConvert.SerializeObject(value);
                    var content = new StringContent(json, Encoding.UTF8, JsonMediaType);

                    var result = await client.PostAsync(requestUri, content);
                    observer.OnNext(result);
                    observer.OnCompleted();
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
            });
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
