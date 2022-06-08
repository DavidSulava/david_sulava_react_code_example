using AutoMapper;
using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Dto.ServerManager.Derivative;
using DesignGear.Contracts.Enums;
using DesignGear.Contracts.Helpers;
using DesignGear.Contracts.Models.ServerManager.Derivative;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DesignGear.Contracts.Communicators
{
    public class ServerManagerCommunicator : IServerManagerCommunicator
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly CommunicatorSettings _settings;

        public ServerManagerCommunicator(IMapper mapper, IHttpClientFactory clientFactory, IOptions<CommunicatorSettings> settings)
        {
            _mapper = mapper;
            _httpClient = clientFactory.CreateClient();
            _settings = settings.Value;
        }

        //public async Task<string> ProcessConfigurationAsync(Guid id)
        //{
        //    return await SendHttpRequestAsync(string.Format($"{_settings.ConfigManagerUrl}automation/{0}", id));
        //}

        private async Task<T> SendHttpRequestAsync<T>(string url)
        {
            var message = await _httpClient.GetAsync(url);
            message.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
        }

        public async Task<string> GetSvfAsync(FileStreamDto packageFile, string rootFileName)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(packageFile.Content), "\"packageFile\"", packageFile.FileName);
            content.Add(new StringContent(rootFileName), "\"rootFileName\"");

            var response = await _httpClient.PostAsync($"{_settings.ServerManagerUrl}derivative", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<SvfStatus> CheckSvfStatusJobAsync(string urn)
        {
            return await SendHttpRequestAsync<SvfStatus>($"{_settings.ServerManagerUrl}derivative/{urn}/status");
        }

        public async Task<byte[]> DownloadSvfAsync(string urn)
        {
            var message = await _httpClient.GetAsync($"{_settings.ServerManagerUrl}derivative/{urn}");
            message.EnsureSuccessStatusCode();
            return await message.Content.ReadAsByteArrayAsync();
        }

        public async Task<VmWorkItem> ProcessModelAsync(byte[] appBundleFile, FileStreamDto packageFile)
        {
            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(appBundleFile), "\"appBundleFile\"", "appbundle");
            content.Add(new StreamContent(packageFile.Content), "\"packageFile\"", packageFile.FileName);

            var response = await _httpClient.PostAsync($"{_settings.ServerManagerUrl}automation", content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<VmWorkItem>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ConfigurationStatus> CheckStatusJobAsync(string workItemId)
        {
            return await SendHttpRequestAsync<ConfigurationStatus>($"{_settings.ServerManagerUrl}automation/{workItemId}/status");
        }

        public async Task<FileStreamDto> DownloadModelAsync(string url)
        {
            var content = (await _httpClient.GetAsync(url)).Content;
            return new FileStreamDto()
            {
                Content = await content.ReadAsStreamAsync(),
                Length = content.Headers.ContentLength.Value,
                ContentType = content.Headers.ContentType.ToString(),
                FileName = content.Headers.ContentDisposition?.FileName.Trim('"')
            };
        }
    }
}
