using Neshagostar.DAL.DataModel;
using Neshagostar.DAL.DataModel.CommerceRelated.OrdersRelated;
using Neshagostar.WebUI.Areas.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neshagostar.WebUI.Areas.Storage.Controllers.OrdersRelated
{
    public class OrdersController : Controller
    {
        const int PAGESIZE = 4;
        NeshagostarContext context = new NeshagostarContext();


        // GET: Commerce/Orders
        public ActionResult Index()
        {
            ViewBag.Products = context.Products.Select(d => new SelectListItem
            {
                Text = d.Title,
                Value = d.Id.ToString()
            });

            // this method gets previous days date starting from today
            List<PersianDateTime> days = GetPreviousDays(5);

            // here we trim the obtained days hour and minutes
            List<string> daysTrimmed = new List<string>();
            foreach (PersianDateTime day in days)
            {
                daysTrimmed.Add(day.ToString().Substring(0, 10));
            }

            // here we put همه تواریخ as the first row in drop down list
            var dates = new List<SelectListItem>();
            dates.Add(new SelectListItem
            {
                Text = "همه تواریخ",
                Value = ""
            });

            // now we put the days in List in order to show in dates drop down list
            ViewBag.Dates = daysTrimmed.OrderByDescending(d => d).Select(d => new SelectListItem
            {
                Text = d,
                Value = d
            });


            // here we get all of the orders so far
            var orders = context.Orders.Include("Customer")
                .Include("OrderItems.Product")
                .Include("OrderItems.Order")
                .Include("OrderItems.OrderItemSendingDetails")
                .OrderByDescending(i => i.Date)
                .Where(o => o.OrderItems.Any(ord => ord.IsDispatched))
                .ToList();
            
            // we use this technique to convert it from reference to value type in order to obtain number of page number calculating from all the numbers of inquiries
            ViewBag.PageNumber = Math.Ceiling(decimal.Parse(orders.Count.ToString()) / (decimal)PAGESIZE);
            ViewBag.Page = 1;
            ViewBag.PageSize = PAGESIZE;

            var result = orders.Take(PAGESIZE).ToList();


            // we convert order items into inquiry bags
            List<List<OrderItemStorageInfo>> ordersStorageInfos = ConvertOrdersToStorageInfoList(result);

            return View("~/Areas/Storage/Views/OrdersRelated/Orders/index.cshtml", ordersStorageInfos);
        }

        private static List<PersianDateTime> GetPreviousDays(double previousdays)
        {
            double numberOfDayspreviousdays = (previousdays + 1) * -1;
            var today = PersianDateTime.Now.Date;
            List<PersianDateTime> days = new List<PersianDateTime>();
            days.Add(today);
            for (int i = -1; i > numberOfDayspreviousdays; i--)
            {
                days.Add(today.AddDays(i));
            }

            return days;
        }


        private List<List<OrderItemStorageInfo>> ConvertOrdersToStorageInfoList(List<Order> orders)
        {
            List<List<OrderItemStorageInfo>> ordersConvertedToStorageInfoList = new List<List<OrderItemStorageInfo>>();
            foreach(var order in orders)
            {
                ordersConvertedToStorageInfoList.Add(ConvertOrderToStorageInfos(order));
            }
            return ordersConvertedToStorageInfoList;
        }

        private List<OrderItemStorageInfo> ConvertOrderToStorageInfos(Order order)
        {
            List<OrderItemStorageInfo> storageInfos = new List<OrderItemStorageInfo>();
            foreach(var orderItem in order.OrderItems)
            {
                storageInfos.Add(ConvertOrderItemToStorageInfo(orderItem));
            }
            return storageInfos;
        }




        private OrderItemStorageInfo ConvertOrderItemToStorageInfo(OrderItem orderItem)
        {
            return new OrderItemStorageInfo
            {
                OrderItemId = orderItem.Id,
                OrderId = orderItem.OrderId,
                CustomerId = orderItem.Order.CustomerId,
                CustomerName = orderItem.Order.Customer.Name,
                DispatchDate = orderItem.DispatchDate,
                LastSendingDate = orderItem.LastSendingDate,
                AmountDispatched = orderItem.AmountDispatched,
                AmountSent = orderItem.AmountSent,
                AmountNotSent = orderItem.AmountNotSent,
                ProductName = orderItem.Product.Title,
                ProductId = orderItem.ProductId
            };
        }
    }
}