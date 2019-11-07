using Neshagostar.DAL.DataModel.ActivityTracker.PersonnelActivity;
using Neshagostar.DAL.DataModel.CallManagement;
using Neshagostar.DAL.DataModel.CommerceRelated.CustomersRelated;
using Neshagostar.DAL.DataModel.CommerceRelated.InquiriesRelated;
using Neshagostar.DAL.DataModel.CommerceRelated.OrdersRelated;
using Neshagostar.DAL.DataModel.CommerceRelated.ProductsRelated;
using Neshagostar.DAL.DataModel.CommerceRelated.TenderRelated;
using Neshagostar.DAL.DataModel.PersonnelRelated;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neshagostar.DAL.FluentAPI
{
    public class EntityTypeConfigs
    {

    }

    #region CallManagement
    public class CallerMapping : EntityTypeConfiguration<Caller>
    {
        public CallerMapping()
        {
            this.HasKey(c => c.Id)
                .HasMany(c => c.RecievedCalls)
                .WithRequired(c => c.Caller)
                .HasForeignKey(c => c.CallerId);                   
        }
    }

    public class RecievedCallMapping : EntityTypeConfiguration<RecievedCall>
    {
        public RecievedCallMapping()
        {
            this.HasKey(c => c.Id);

        }
    }

    public class DepartmentMapping : EntityTypeConfiguration<Department>
    {
        public DepartmentMapping()
        {
            this.HasKey(d => d.Id)
                .HasMany(d => d.Personnels)
                .WithRequired(p => p.Department)
                .HasForeignKey(p => p.DepartmentId);
        }
    }
    #endregion

    #region CommerceRelated

    public class OrderMapping : EntityTypeConfiguration<Order>
    {
        public OrderMapping()
        {
            this.HasKey(c => c.Id)
                .HasMany(o => o.OrderItems)
                .WithRequired(o => o.Order)
                .HasForeignKey(o => o.OrderId);

            this.HasRequired(o => o.Customer)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.CustomerId);
        }
    }

    public class OrderItemMapping : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMapping()
        {
            this.HasKey(c => c.Id)
                .HasRequired(o => o.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId);

            this.HasRequired(o => o.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(o => o.ProductId);
        }
    }


    public class TenderItemMapping : EntityTypeConfiguration<TenderItem>
    {
        public TenderItemMapping()
        {
            this.HasKey(c => c.Id)
                .HasRequired(a => a.Tender)
                .WithMany(p => p.TenderItems)
                .HasForeignKey(a => a.TenderId);

            this.HasRequired(t => t.Product)
                .WithMany()
                .HasForeignKey(t => t.ProductId);

        }
    }

    public class TenderMapping : EntityTypeConfiguration<Tender>
    {
        public TenderMapping()
        {
            this.HasKey(c => c.Id)
                .HasMany(t => t.TenderItems)
                .WithRequired(t => t.Tender)
                .HasForeignKey(t => t.TenderId);

            this.HasRequired(t => t.Customer)
                .WithMany()
                .HasForeignKey(t => t.CustomerId);

            this.HasMany(t => t.RivalPrices)
                .WithRequired(r => r.Tender)
                .HasForeignKey(r => r.TenderId);
                
        }
    }

    public class RivalMapping : EntityTypeConfiguration<Rival>
    {
        public RivalMapping()
        {
            this.HasKey(o => o.Id)
                .HasMany(o => o.RivalPrices)
                .WithRequired(t => t.Rival)
                .HasForeignKey(o => o.RivalId);
        }
    }

    public class RivalPriceMapping : EntityTypeConfiguration<RivalPrice>
    {
        public RivalPriceMapping()
        {
            this.HasKey(o => o.Id)
            .HasRequired(t => t.Rival)
            .WithMany(t => t.RivalPrices)
            .HasForeignKey(t => t.RivalId);
        }
    }


    public class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        public CustomerMapping()
        {
            this.HasKey(c => c.Id);
        }
    }


    public class ProductCategoryMapping : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMapping()
        {
            this.HasKey(c => c.Id);
        }
    }

    public class RingStiffnessMapping : EntityTypeConfiguration<RingStiffness>
    {
        public RingStiffnessMapping()
        {
            this.HasKey(c => c.Id);
        }
    }

    public class PipeDiameterMapping : EntityTypeConfiguration<PipeDiameter>
    {
        public PipeDiameterMapping()
        {
            this.HasKey(c => c.Id);
        }
    }

    public class PipeProfileMapping : EntityTypeConfiguration<PipeProfile>
    {
        public PipeProfileMapping()
        {
            this.HasKey(c => c.Id);
        }
    }

    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            this.HasKey(c => c.Id)
                .HasRequired(p => p.PipeDiameter)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.PipeDiameterId)
                .WillCascadeOnDelete();

            this.HasRequired(p => p.PipeProfile)
              .WithMany(p => p.Products)
              .HasForeignKey(p => p.PipeProfileId)
              .WillCascadeOnDelete();

            this.HasRequired(p => p.ProductCategory)
           .WithMany(p => p.Products)
           .HasForeignKey(p => p.ProductCategoryId)
           .WillCascadeOnDelete();

            this.HasRequired(p => p.RingStiffness)
           .WithMany(p => p.Products)
           .HasForeignKey(p => p.RingStiffnessId)
           .WillCascadeOnDelete();

        }
    }



    public class InquiryItemMapping : EntityTypeConfiguration<InquiryItem>
    {
        public InquiryItemMapping()
        {
            this.HasKey(c => c.Id)
                .HasRequired(i => i.Inquiry)
                .WithMany(i => i.InquiryItems)
                .HasForeignKey(i => i.InquiryId);

            this.HasRequired(i => i.Product)
                .WithMany(p => p.InquiryItems)
                .HasForeignKey(i => i.ProductId);
        }
    }

    public class InquiryMapping : EntityTypeConfiguration<Inquiry>
    {
        public InquiryMapping()
        {
            this.HasKey(c => c.Id)
                .HasRequired(c => c.Customer)
                .WithMany(c => c.Inquiries)
                .HasForeignKey(i => i.CustomerId);        
        }
    }

    #endregion

    #region ActivityLogging

    public class ActivityLogMapping : EntityTypeConfiguration<ActivityLog>
    {
        public ActivityLogMapping()
        {
            this.HasKey(c => c.Id)
                .HasRequired(a => a.Personnel)
                .WithMany(p => p.ActivityLogs)
                .HasForeignKey(a => a.PersonnelId);                          
        }
    }

    #endregion
}
