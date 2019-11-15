using Neshagostar.DAL.DataModel.CommerceRelated.OrdersRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neshagostar.DAL.DataModel.StorageRelated.OrdersRelated
{
    public class OrderItemSendingDetail
    {
        public Guid Id { get; set; }
        public Guid OrderItemId { get; set; }

        [Display(Name = "تاریخ ارسال بار")]
        public string DateTime { get; set; }
        
        [Display(Name = "مقدار ارسال شده")]
        public double SendingAmount { get; set; }

        [Display(Name = "شماره بارنامه")]
        public string CarrierNumberCode { get; set; }

        [Display(Name = "توضیحات")]
        public string Comments { get; set; }

        public OrderItem OrderItem { get; set; }
    }
}
