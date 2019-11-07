using Neshagostar.DAL.DataModel;
using Neshagostar.DAL.DataModel.CommerceRelated.OrdersRelated;
using Neshagostar.WebUI.Areas.Commerce.Models.OrdersRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Neshagostar.WebUI.Areas.Commerce.Controllers.OrdersRelated
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
            List<Order> orders = context.Orders.Include("Customer").Include("OrderItems.Product").Include("OrderItems.Order").OrderByDescending(i => i.Date).ToList();

            // we use this technique to convert it from reference to value type in order to obtain number of page number calculating from all the numbers of inquiries
            ViewBag.PageNumber = Math.Ceiling(decimal.Parse(orders.Count.ToString()) / (decimal)PAGESIZE);
            ViewBag.Page = 1;
            ViewBag.PageSize = PAGESIZE;

            var result = orders.Take(PAGESIZE).ToList();


            // we convert order items into inquiry bags
            List<List<OrderBag>> orderBags = ConvertOrdersToOrderBag(result);

            return View("~/Areas/Commerce/Views/OrdersRelated/Orders/index.cshtml", orderBags);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Products = new SelectList(context.Products, "Id", "Name");
            return View("~/Areas/Commerce/Views/OrdersRelated/Orders/Create.cshtml");
        }


        [HttpPost]
        public JsonResult Create(Order order)
        {

            order.Id = Guid.NewGuid();
            order.Date = PersianDateTime.Now.ToString();
            

            foreach (OrderItem item in order.OrderItems)
            {
                item.Id = Guid.NewGuid();
                item.OrderId = order.Id;
            }

            context.Orders.Add(order);
            context.SaveChanges();
            ViewBag.ModelOperatedId = order.Id.ToString();


            // we return return url in json because in ajax success callback function it's been used in windows.location method
            return Json(Url.Action("Index", "Orders", new { area = "Commerce" }));
        }

        [HttpPost]
        public JsonResult Filter(string date, string customerName, string productId, int page = 1)
        {
            IQueryable<Order> orders = context.Orders.Include("Customer").Include("OrderItems.Product");

            if (date != "" && date != null)
            {
                string[] modelarray = date.Split('/');
                PersianDateTime dateModel = new PersianDateTime(int.Parse(modelarray[0]), int.Parse(modelarray[1]), int.Parse(modelarray[2]));
                string dateInModel = dateModel.Date.ToString().Substring(0, 10);

                orders = orders.Where(i => i.Date.Substring(0, 10).Equals(dateInModel));
            }

            if (customerName != "" && customerName != null)
            {
                orders = orders.Where(i => i.Customer.Name.Contains(customerName));
            }

            var allOrders = orders.ToList();

            if (productId != "")
            {
                // first we get the list of inquiry Items that has the defined product id
                var convertedProductId = Guid.Parse(productId);
                var orderItems = context.OrderItems.Where(i => i.ProductId.Equals(convertedProductId)).ToList();
                // and then we filter inquiries based on the id of inquiryItems that we have found.
                if (orderItems != null)
                {
                    List<Order> inqToList = new List<Order>();
                    List<Order> ordersWithSameProductId = new List<Order>();
                    foreach (var item in orderItems)
                    {
                        ordersWithSameProductId.AddRange(allOrders.Where(i => i.Id == item.OrderId));

                    }
                    allOrders = allOrders.Intersect(ordersWithSameProductId).ToList();
                }
            }

            var ordersList = allOrders.OrderByDescending(i => i.Date).ToList();
            var pageNumbers = Math.Ceiling(ordersList.Count / (decimal)PAGESIZE);
            ordersList = ordersList.Skip((page - 1) * PAGESIZE).Take(PAGESIZE).ToList();

            if (page > pageNumbers)
            {
                page = Int32.Parse((pageNumbers).ToString());
            }

            List<List<OrderBag>> result = ConvertOrdersToOrderBag(ordersList);

            if(result.Count > 0)
            {
                result.ElementAt(0).ElementAt(0).Page = page;
                result.ElementAt(0).ElementAt(0).PageNumbers = pageNumbers;
            }
      
            //foreach (var inq in inquiriesList)
            //{
            //    result.Add(ConvertInquiryItemsToInquiryBag(inq.InquiryItems));

              
            //}

            //if (result.Count == 0)
            //{
            //    var bag = new InquiryBag();
            //    var inquiryList = new List<InquiryBag>();
            //    inquiryList.Add(bag);
            //    result.Add(inquiryList);
            //}

            return Json(result, JsonRequestBehavior.AllowGet);



        }

        private OrderBag ConvertIOrderItemToOrderBag(OrderItem item)
        {
            return new OrderBag
            {
                CustomerId = item.Order.CustomerId,
                CustomerName = item.Order.Customer.Name,
                CustomerCity = item.Order.Customer.City,
                CustomerTel = item.Order.Customer.TelephoneNumber,
                Id = item.Id,
                //InquiryId = item.Order.InquiryId,
                ProductId = item.ProductId,
                DateTime = item.Order.Date.Substring(0, 10),
                ProductName = item.Product.Title,
                Amount = item.Amount,
                PricePerUnit = item.PricePerUnit,
                PricePerKilo = item.PricePerKilo,
                TotalPrice = item.TotalPrice,
                TotalWeight = item.TotalWeight,
                HDPEPrice = item.HDPEPrice,
                NominalWeightPerMeter = item.NominalWeightPerMeter,
                Comments = item.Comments,
                WasherPrice = item.WasherPrice,
                InquiryTotalPrice = item.Order.OrderItemsPriceSummation,
                IsDispatched = item.IsDispatched
            };
        }

        public List<List<OrderBag>> ConvertOrdersToOrderBag(List<Order> orders)
        {
            List<List<OrderBag>> orderbags = new List<List<OrderBag>>();

            foreach (Order order in orders)
            {
                List<OrderBag> bags = new List<OrderBag>();
                foreach (var orderItem in order.OrderItems)
                {
                    bags.Add(ConvertIOrderItemToOrderBag(orderItem));
                }
                orderbags.Add(bags);
            }

            return orderbags;
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



    }
}