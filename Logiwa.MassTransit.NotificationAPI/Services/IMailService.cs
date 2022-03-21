using Logiwa.MassTransit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logiwa.MassTransit.NotificationAPI.Services
{
    public interface IMailService
    {
        public void SendMail(SubmitOrder order);
    }
}
