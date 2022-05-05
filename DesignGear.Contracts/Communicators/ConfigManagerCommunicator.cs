using DesignGear.Contracts.Communicators.Interfaces;
using Newtonsoft.Json;

namespace DesignGear.Contracts.Communicators
{
    public class ConfigManagerCommunicator : IConfigManagerCommunicator
    {
        private readonly HttpClient _httpClient;
        //private readonly AppOptions _options;

        public ConfigManagerCommunicator(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
        }
        public async Task<string> ProcessConfigurationAsync(Guid id)
        {
            return await SendHttpRequestAsync(string.Format("https://localhost:5001/automation/{0}", id));
        }

        public async Task<string> GetSvfAsync(Guid id)
        {
            return await SendHttpRequestAsync(string.Format("https://localhost:5001/automation/{0}", id));
        }

        private async Task<T> SendHttpRequestAsync<T>(string url)
        {
            var message = await _httpClient.GetAsync(url);
            message.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
        }

        private async Task<string> SendHttpRequestAsync(string url)
        {
            var message = await _httpClient.GetAsync(url);
            message.EnsureSuccessStatusCode();
            return await message.Content.ReadAsStringAsync();
        }
    }
}
