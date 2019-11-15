using Neshagostar.DAL.DataModel.StorageRelated.OrdersRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neshagostar.DAL.DataModel.CommerceRelated.OrdersRelated
{
    public partial class OrderItem
    {
        public string DispatchDate { get; set; }

        [Display(Name = "مقدار ارسال شده")]
        public double AmountSent
        {
            get
            {
                if (OrderItemSendingDetails != null)
                {
                    double sum = 0;
                    foreach (OrderItemSendingDetail sendingDetail in OrderItemSendingDetails)
                    {
                        sum += sendingDetail.SendingAmount;
                    }
                    return sum;
                }
                else
                {
                    return 0;
                }
            }
        }


        [Display(Name = "مقدار ارسال نشده")]
        public double AmountNotSent
        {
            get
            {
                return Amount - AmountSent;
            }
        }

        [Display(Name = "آخرین تاریخ ارسال")]
        public string LastSendingDate
        {
            get
            {
                if(OrderItemSendingDetails != null && OrderItemSendingDetails.Count != 0)
                {
                   return OrderItemSendingDetails.OrderByDescending(o => o.DateTime).FirstOrDefault().DateTime;
                }
                else
                {
                    return "";
                }
            }
        }


        public List<OrderItemSendingDetail> OrderItemSendingDetails { get; set; }

    }
}
