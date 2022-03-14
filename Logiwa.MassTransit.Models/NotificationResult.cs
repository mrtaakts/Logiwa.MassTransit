using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logiwa.MassTransit.Models
{
    public class NotificationResult
    {
        public int OrderId { get; set; }
        public string message { get; set; }
        public DateTime CreatedDate { get; set;}

        public override string ToString()
        {
            return $"OrderId: {OrderId} {message}";
        }
    }
}
