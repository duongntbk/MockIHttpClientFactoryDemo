using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MockIHttpClientFactoryDemo
{
    public class PassPwnedCheckClient : IPassPwnedCheckClient
    {
        private readonly Uri BaseUri = new Uri("https://api.pwnedpasswords.com/");
        private readonly HttpClient _httpClient;

        public PassPwnedCheckClient(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory.CreateClient(Constants.HttpClientName);

        public async Task<Dictionary<string, int>> GetHashes(string prefix)
        {
            var fullUri = new Uri(BaseUri, $"range/{prefix}");
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = fullUri,
            };

            var response = await _httpClient.SendAsync(request);
            var rawHashes = await response.Content.ReadAsStringAsync();

            return rawHashes.Split("\r\n").Select(r => r.Split(":")).ToDictionary(r => r[0], r => int.Parse(r[1]));
        }
    }
}
