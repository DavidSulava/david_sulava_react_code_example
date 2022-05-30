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

        public async Task<string> GetSvfAsync(FileStreamDto packageFile, string rootFileName)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(rootFileName), "\"rootFileName\"");
            content.Add(new StreamContent(packageFile.Content), "\"packageFile\"", packageFile.FileName);

            var response = await _httpClient.PostAsync($"{_settings.ServerManagerUrl}derivative", content);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
