using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Coupons.PepsiKSA.Api.Integrations.HttpFactory
{
    public class HttpFactory : IHttpFactory
    {
        public async Task<T> ClientGetAsync<T>(string uri)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            using HttpResponseMessage response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public async Task<TOut> ClientPostAsync<TIn, TOut>(string uri, TIn content)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await client.PostAsync(uri, serialized);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TOut>(responseBody);
        }
        public async Task<TOut> ClientPostAsync<TIn, TOut>(string uri, TIn content, Dictionary<string, string> customHeader)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            foreach (KeyValuePair<string, string> entry in customHeader)
            {
                client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
            }
            var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await client.PostAsync(uri, serialized);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TOut>(responseBody);
        }
    }
}
