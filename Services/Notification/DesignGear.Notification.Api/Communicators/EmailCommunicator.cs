using DesignGear.Contracts.Models.Notification;
using DesignGear.Notification.Api.Options;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace DesignGear.Notification.Api.Communicators {
    public class EmailCommunicator {
        private readonly ILogger<EmailCommunicator> _logger;
        private readonly EmailOptions _emailOptions;
        private readonly Lazy<SmtpClient> _smtpClient;

        private const string delimiter = ";";

        public string DefaultFromAddress => _emailOptions.FromAddress;

        public EmailCommunicator(IOptions<EmailOptions> settings, ILogger<EmailCommunicator> logger) {
            _emailOptions = settings.Value;
            _logger = logger;
            _smtpClient = new Lazy<SmtpClient>(() => new SmtpClient {
                Host = _emailOptions.Server,
                Port = _emailOptions.Port,
                UseDefaultCredentials = _emailOptions.UseDefaultCredentials,
                Credentials = new NetworkCredential(_emailOptions.UserName, _emailOptions.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = _emailOptions.EnableSsl
            });
        }

        public async Task<bool> SendEmailAsync(EmailRequestModel request) {
            var isSent = false;
            try {
                var mail = new MailMessage();
                mail.From = new MailAddress(_emailOptions.FromAddress);
                mail.Subject = request.Topic;
                mail.Body = request.Message;
                mail.IsBodyHtml = request.IsBodyHtml;

                var addresses = request.TargetAddress.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                foreach (var address in addresses) {
                    mail.To.Add(new MailAddress(address));
                }

                using (var smtp = _smtpClient.Value) {
                    await smtp.SendMailAsync(mail);
                }

                isSent = true;
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message, ex);
            }

            return isSent;
        }
    }
}
