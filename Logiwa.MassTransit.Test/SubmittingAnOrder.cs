using Logiwa.MassTransit.Models;
using MassTransit;
using MassTransit.Testing;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Logiwa.MassTransit.Test
{
    public class Submitting_an_order
    {

        [Test]
        public async Task Should_consume_the_order_submitted_event()
        {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<OrderConsumer>();

            await harness.Start();
            try
            {
                await harness.InputQueueSendEndpoint.Send<SubmitOrder>(new SubmitOrder
                {
                    Id = 3,
                    CreatedDate = DateTime.Now,
                    Name = "mert"
                });

                // did the endpoint consume the message
                Assert.That(await harness.Consumed.Any<SubmitOrder>());

                // did the actual consumer consume the message
                Assert.That(await consumerHarness.Consumed.Any<SubmitOrder>());

            }
            finally
            {
                await harness.Stop();
            }
        }
    }

    public class OrderConsumer : IConsumer<SubmitOrder>
    {
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            Console.WriteLine("Value: {0}", context.Message.ToString());
        }

    }
}