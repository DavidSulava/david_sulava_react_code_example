using AutoMapper;
using DesignGear.Common.Extensions;
using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Helpers;
using DesignGear.Contracts.Models.ConfigManager;
using Kendo.Mvc.UI;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace DesignGear.Contracts.Communicators
{
    public class ConfigManagerCommunicator : IConfigManagerCommunicator
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly CommunicatorSettings _settings;

        public ConfigManagerCommunicator(IMapper mapper, IHttpClientFactory clientFactory, IOptions<CommunicatorSettings> settings)
        {
            _mapper = mapper;
            _httpClient = clientFactory.CreateClient();
            _settings = settings.Value;
        }
        public async Task<string> ProcessConfigurationAsync(Guid id)
        {
            return await SendHttpRequestAsync(string.Format($"{_settings.ConfigManagerUrl}automation/{0}", id));
        }

        public async Task<string> GetSvfAsync(Guid id)
        {
            return await SendHttpRequestAsync(string.Format($"{_settings.ConfigManagerUrl}derivative/{0}", id));
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

        public async Task<ICollection<AppBundleDto>> GetAppBundleListAsync()
        {
            return (await SendHttpRequestAsync<ICollection<VmAppBundleItem>>($"{_settings.ConfigManagerUrl}appbundle")).MapTo<ICollection<AppBundleDto>>(_mapper);
        }

        public async Task CreateConfigurationAsync(VmConfigurationCreate create)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(create.OrganizationId.ToString()), "\"OrganizationId\"");
            content.Add(new StringContent(create.ProductId.ToString()), "\"ProductId\"");
            content.Add(new StringContent(create.ProductVersionId.ToString()), "\"ProductVersionId\"");
            content.Add(new StringContent(create.AppBundleId.ToString()), "\"AppBundleId\"");
            content.Add(new StreamContent(create.ConfigurationPackage.OpenReadStream()), "\"ConfigurationPackage\"", create.ConfigurationPackage.FileName);

            var response = await _httpClient.PostAsync($"{_settings.ConfigManagerUrl}configuration", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateConfigurationRequestAsync(VmConfigurationRequest request)
        {
            var response = await _httpClient.PostAsync($"{_settings.ConfigManagerUrl}configuration/request", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateConfigurationAsync(ConfigurationUpdateDto update)
        {
            var response = await _httpClient.PutAsync($"{_settings.ConfigManagerUrl}configuration", new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task<FileStreamDto> GetSvfAsync(Guid configurationId, string svfName)
        {
            return await SendHttpRequestAsync<FileStreamDto>($"{_settings.ConfigManagerUrl}configuration/{configurationId}/svf{svfName}");
        }

        public async Task<string> GetSvfRootFileNameAsync(Guid configurationId)
        {
            return await SendHttpRequestAsync<string>($"{_settings.ConfigManagerUrl}configuration/{configurationId}/svf");
        }

        public async Task<Dto.ConfigManager.ConfigurationParametersDto> GetConfigurationParametersAsync(Guid configurationId)
        {
            return (await SendHttpRequestAsync<VmComponentParameterDefinitions>($"{_settings.ConfigManagerUrl}configuration/{configurationId}/parameters")).MapTo<Dto.ConfigManager.ConfigurationParametersDto>(_mapper);
        }

        public async Task<DataSourceResult> GetConfigurationItemsAsync(string queryString)
        {
            return (await SendHttpRequestAsync<DataSourceResult>($"{_settings.ConfigManagerUrl}configuration{queryString}"));
        }
    }
}
