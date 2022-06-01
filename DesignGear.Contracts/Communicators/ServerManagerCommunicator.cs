using AutoMapper;
using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.Contracts.Dto;
using DesignGear.Contracts.Helpers;
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
            /*using (var memoryStream = new MemoryStream())
            {
                await packageFile.Content.CopyToAsync(memoryStream);
                content.Add(new ByteArrayContent(memoryStream.ToArray()), "\"packageFile\"", packageFile.FileName);
            }*/
            content.Add(new StringContent(rootFileName), "\"rootFileName\"");

            var response = await _httpClient.PostAsync($"{_settings.ServerManagerUrl}derivative", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CheckStatusJobAsync(string urn)
        {
            var message = await _httpClient.GetAsync($"{_settings.ServerManagerUrl}derivative/{urn}");
            message.EnsureSuccessStatusCode();
            return await message.Content.ReadAsStringAsync();
        }
    }
}
