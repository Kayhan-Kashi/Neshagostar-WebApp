using Neshagostar.DAL.DataModel.CommerceRelated.ProductsRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neshagostar.DAL.DataModel.CommerceRelated.OrdersRelated
{
    public class OrderItem
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        #endregion


        #region Commerce
        [Display(Name = "مقدار")]
        public double Amount { get; set; }

        [Display(Name = "قیمت به ازای واحد")]
        public double PricePerUnit { get; set; }

        [Display(Name = "وزن اسمی")]
        public double NominalWeightPerMeter { get; set; }

        [Display(Name = "مجموع وزن")]
        public double TotalWeight { get; set; }

        [Display(Name = "قیمت مواد اولیه")]
        public double HDPEPrice { get; set; }

        [Display(Name = "قیمت واشر")]
        public double WasherPrice { get; set; }

        [Display(Name = "آیا ابلاغ شده است ؟")]
        public bool IsDispatched { get; set; }

        [Display(Name = "مقدار ابلاغ شده")]
        public double AmountDispatched { get; set; }

        [Display(Name = "قیمت کل آیتم سفارش ")]
        public double TotalPrice { get; set; }

        [Display(Name = "قیمت به ازای هر کیلو ")]
        public double PricePerKilo
        {
            get
            {
                return Math.Ceiling(TotalPrice / TotalWeight);
            }
        }

        [Display(Name = "توضیحات")]
        public string Comments { get; set; }

        [Display(Name = "تاریخ تحویل خواسته شده از سوی مشتری")]
        public string DateToRecieve { get; set; }

        #endregion

        #region Warehouse

        #endregion

        #region Navigational properties
        public Order Order { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
