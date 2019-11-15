using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Neshagostar.WebUI.Areas.Storage.Models
{
    public class OrderItemStorageInfo
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        
        public double AmountSent { get; set; }
        public double AmountNotSent { get; set; }

        public double AmountDispatched { get; set; }

        public string DispatchDate { get; set; }

        public string LastSendingDate { get; set; }
    }
}