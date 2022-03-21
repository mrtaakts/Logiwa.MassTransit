using Logiwa.MassTransit.NotificationAPI.Services;
using Logiwa.MassTransit.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logiwa.MassTransit.NotificationAPI
{
    public class OrderConsumer : IConsumer<SubmitOrder>
    {
        private readonly IMailService _mailService;

        public OrderConsumer(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {

            Console.WriteLine("Value: {0}", context.Message.ToString());

            if (context.Message.Id > 0)
            {
                await context.RespondAsync<OrderResult>(
                new OrderResult
                {
                    OrderId = context.Message.Id,
                    message = "Order added Sending email.",
                    StatusCode = 200,
                    CreatedDate = DateTime.Now

                });
                _mailService.SendMail(context.Message);
            }
            else
            {
                await context.RespondAsync<OrderResult>(
                new OrderResult
                {
                    OrderId = context.Message.Id,
                    message = "Failed to add order",
                    StatusCode = 500,
                    CreatedDate = DateTime.Now
                });
            }
        }
    }
}
