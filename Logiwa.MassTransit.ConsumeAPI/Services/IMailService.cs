﻿using Logiwa.MassTransit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logiwa.MassTransit.ConsumeAPI.Services
{
    public interface IMailService
    {
        public string SendMail(SubmitOrder order);
    }
}
