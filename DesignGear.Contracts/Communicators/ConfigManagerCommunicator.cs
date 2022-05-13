using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.Contracts.Dto;
using Newtonsoft.Json;

namespace DesignGear.Contracts.Communicators
{
    public class ConfigManagerCommunicator : IConfigManagerCommunicator
    {
        private readonly string _configMgrUrl = "https://localhost:7029/";
        private readonly HttpClient _httpClient;
        //private readonly AppOptions _options;

        public ConfigManagerCommunicator(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
        }
        public async Task<string> ProcessConfigurationAsync(Guid id)
        {
            return await SendHttpRequestAsync(string.Format($"{_configMgrUrl}automation/{0}", id));
        }

        public async Task<string> GetSvfAsync(Guid id)
        {
            return await SendHttpRequestAsync(string.Format($"{_configMgrUrl}derivative/{0}", id));
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

        //public async Task CreateConfigurationAsync(CreateConfigurationRequest create)
        //{
        //    var content = new MultipartFormDataContent();
        //    content.Add(new StringContent(create.OrganizationId.ToString()), "\"OrganizationId\"");
        //    content.Add(new StringContent(create.ProductId.ToString()), "\"ProductId\"");
        //    content.Add(new StringContent(create.ProductVersionId.ToString()), "\"ProductVersionId\"");
        //    content.Add(new ByteArrayContent(create.ModelFile.Content), "\"ModelFile\"", create.ModelFile.FileName);

        //    var response = await _httpClient.PostAsync("https://localhost:7029/configuration", content);
        //    response.EnsureSuccessStatusCode();
        //}
            

        public async Task<ICollection<AppBundleDto>> GetAppBundleListAsync()
        {
            return await SendHttpRequestAsync<ICollection<AppBundleDto>>($"{_configMgrUrl}appbundle");
        }
    }
}
