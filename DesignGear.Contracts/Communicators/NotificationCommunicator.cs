using DesignGear.Contracts.Communicators.Interfaces;
using DesignGear.Contracts.Models.Notification;
using DesignGear.Contracts.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Communicators {
    public class NotificationCommunicator : INotificationCommunicator {
        private readonly HttpClient _httpClient;
        private readonly NotificationOptions _notificationOptions;

        public NotificationCommunicator(IHttpClientFactory httpClientFactory, IOptions<NotificationOptions> notificationOptions) {
            _notificationOptions = notificationOptions.Value;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task SendEmailAsync(EmailRequestModel request) {
            await _httpClient.PostAsync($"{_notificationOptions.ServiceUrl}email", new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));
        }
    }
}
