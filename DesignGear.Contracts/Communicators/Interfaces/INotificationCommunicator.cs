using DesignGear.Contracts.Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignGear.Contracts.Communicators.Interfaces {
    public interface INotificationCommunicator {
        Task SendEmailAsync(EmailRequestModel request);
    }
}
