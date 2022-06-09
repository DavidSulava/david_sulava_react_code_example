using AutoMapper;
using DesignGear.Common.Extensions;
using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ConfigManager;
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

        private async Task<T> SendHttpRequestAsync<T>(string url)
        {
            var message = await _httpClient.GetAsync(url);
            message.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
        }

        private async Task<T> SendHttpRequestJsonAsync<T>(string url)
        {
            var message = await _httpClient.GetAsync(url);
            message.EnsureSuccessStatusCode();
            var result = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(result, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
        }

        private async Task<string> SendHttpRequestAsync(string url)
        {
            var message = await _httpClient.GetAsync(url);
            message.EnsureSuccessStatusCode();
            return await message.Content.ReadAsStringAsync();
        }

        public async Task<Guid> CreateAppBundleAsync(CreateAppBundleDto create)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(create.Name), "\"Name\"");
            content.Add(new StringContent(create.Description), "\"Description\"");
            content.Add(new StringContent(create.DesignGearVersion), "\"DesignGearVersion\"");
            content.Add(new StringContent(create.InventorVersion), "\"InventorVersion\"");
            content.Add(new StreamContent(create.File.OpenReadStream()), "\"File\"", create.File.FileName);

            var response = await _httpClient.PostAsync($"{_settings.ConfigManagerUrl}appbundle", content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
        }

        public async Task UpdateAppBundleAsync(UpdateAppBundleDto update)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(update.Id.ToString()), "\"Id\"");
            content.Add(new StringContent(update.Name), "\"Name\"");
            content.Add(new StringContent(update.Description), "\"Description\"");
            content.Add(new StringContent(update.DesignGearVersion), "\"DesignGearVersion\"");
            content.Add(new StringContent(update.InventorVersion), "\"InventorVersion\"");
            if (update.File != null)
                content.Add(new StreamContent(update.File.OpenReadStream()), "\"File\"", update.File.FileName);

            var response = await _httpClient.PutAsync($"{_settings.ConfigManagerUrl}appbundle", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveAppBundleAsync(Guid id)
        {
            var message = await _httpClient.DeleteAsync($"{_settings.ConfigManagerUrl}appbundle?id={id}");
            message.EnsureSuccessStatusCode();
        }

        public async Task<ICollection<AppBundleDto>> GetAppBundleListAsync()
        {
            return (await SendHttpRequestAsync<ICollection<VmAppBundleItem>>($"{_settings.ConfigManagerUrl}appbundle")).MapTo<ICollection<AppBundleDto>>(_mapper);
        }

        public async Task<AppBundleDto> GetAppBundleAsync(Guid id)
        {
            return (await SendHttpRequestAsync<VmAppBundleItem>($"{_settings.ConfigManagerUrl}appbundle/{id}")).MapTo<AppBundleDto>(_mapper);
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

        public async Task<Guid> CreateConfigurationRequestAsync(VmConfigurationRequest request)
        {
            var response = await _httpClient.PostAsync($"{_settings.ConfigManagerUrl}configuration/request", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
        }

        //public async Task UpdateConfigurationAsync(ConfigurationUpdateDto update)
        //{
        //    var response = await _httpClient.PutAsync($"{_settings.ConfigManagerUrl}configuration", new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json"));
        //    response.EnsureSuccessStatusCode();
        //}

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
            return (await SendHttpRequestAsync<VmComponentParameterDefinitions>($"{_settings.ConfigManagerUrl}configuration/{configurationId}/parameters")).MapTo<ConfigurationParametersDto>(_mapper);
        }

        public async Task<DataSourceResult> GetConfigurationItemsAsync(string queryString)
        {
            return (await SendHttpRequestJsonAsync<DataSourceResult>($"{_settings.ConfigManagerUrl}configuration{queryString}"));
        }

        public async Task<ConfigurationDto> GetConfigurationAsync(Guid id)
        {
            return (await SendHttpRequestJsonAsync<ConfigurationDto>($"{_settings.ConfigManagerUrl}configuration/{id}")).MapTo<ConfigurationDto>(_mapper);
        }
    }
}
