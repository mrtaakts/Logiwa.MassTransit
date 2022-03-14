using Logiwa.MassTransit.Models;
using Logiwa.MassTransit.OrderAPI;
using Logiwa.MassTransit.OrderAPI.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logiwa.MassTransit.OrderAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;
        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
          
        }

        public async Task<int> AddOrder(SubmitOrder order)
        {
             await _dbContext.AddAsync(order);
            var resp = await _dbContext.SaveChangesAsync();
            if (resp > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
