using Logiwa.MassTransit.Models;
using Logiwa.MassTransit.OrderAPI.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Logiwa.MassTransit.OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        readonly IRequestClient<SubmitOrder> _client;
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, IRequestClient<SubmitOrder> client)
        {
            _orderService = orderService;
            _client = client;

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubmitOrder order)
        {
            await _orderService.AddOrder(order);
            var response = await _client.GetResponse<OrderResult>(order);
            return Ok(response.Message);
        }


    }
}
