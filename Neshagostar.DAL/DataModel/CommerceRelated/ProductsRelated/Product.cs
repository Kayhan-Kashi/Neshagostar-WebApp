using Neshagostar.DAL.DataModel.CommerceRelated.InquiriesRelated;
using Neshagostar.DAL.DataModel.CommerceRelated.OrdersRelated;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neshagostar.DAL.DataModel.CommerceRelated.ProductsRelated
{
    public class Product
    {
        #region Keys
        public Guid Id { get; set; }
        public Guid ProductCategoryId { get; set; }
        public Guid PipeProfileId { get; set; }
        public Guid PipeDiameterId { get; set; }
        public Guid RingStiffnessId { get; set; }
        #endregion

        #region Scalar properties
        [Display(Name = "عنوان محصول")]
        public string Title { get; set; }
        #endregion

        #region Navigational Properties
        public ProductCategory ProductCategory { get; set; }
        public PipeDiameter PipeDiameter { get; set; }
        public PipeProfile PipeProfile { get; set; }
        public RingStiffness RingStiffness { get; set; }
        public List<InquiryItem> InquiryItems { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        #endregion

    }
}
