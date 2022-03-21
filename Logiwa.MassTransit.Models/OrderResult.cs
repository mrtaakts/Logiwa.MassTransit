using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logiwa.MassTransit.Models
{
    public class OrderResult
    {
        public int OrderId { get; set; }
        public string message { get; set; }
        public int StatusCode { get; set; }
        public DateTime CreatedDate { get; set;}

    
    }
}
