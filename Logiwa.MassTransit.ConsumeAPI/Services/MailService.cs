using Logiwa.MassTransit.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logiwa.MassTransit.ConsumeAPI.Services
{

    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SendMail(SubmitOrder order)
        {
            MimeMessage mail = new MimeMessage();

            MailboxAddress to = new MailboxAddress("User",
            _configuration["Smtp:to"]);
            mail.To.Add(to);

            MailboxAddress from = new MailboxAddress("Mert",
            _configuration["Smtp:from"]);
            mail.From.Add(from);

            mail.Subject = "Order email";

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = order.ToString();
            mail.Body = bodyBuilder.ToMessageBody();

            SmtpClient smtp = new SmtpClient();
            smtp.Connect(_configuration["Smtp:link"], 587, false);
            smtp.Authenticate(_configuration["Smtp:username"], _configuration["Smtp:password"]);

            var resp = smtp.Send(mail);
            smtp.Disconnect(true);
            smtp.Dispose();

            return "Mail Gönderildi";
        }
    }
}
