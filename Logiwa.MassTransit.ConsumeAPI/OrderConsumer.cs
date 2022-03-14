using Logiwa.MassTransit.ConsumeAPI.Services;
using Logiwa.MassTransit.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logiwa.MassTransit.ConsumeAPI
{
    public class OrderConsumer : IConsumer<SubmitOrder>
    {
        private readonly IOrderService _orderService;

        public OrderConsumer(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            var resp = await _orderService.AddOrder(context.Message);
            if(resp != 1) throw new InvalidOperationException("Order cannot added");

            Console.WriteLine("Value: {0}", context.Message.ToString());
        }

    }
}
