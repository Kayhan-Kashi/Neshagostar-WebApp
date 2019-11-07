using Microsoft.AspNet.Identity.EntityFramework;
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
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neshagostar.DAL.DataModel
{
    public class NeshagostarContext : IdentityDbContext
    {
        public NeshagostarContext() : base("NeshagostarDatabaseVer2")
        {


        }

        public static NeshagostarContext Create()
        {
            return new NeshagostarContext();
        }


        public DbSet<Personnel> IdentityUsers { get; set; }
        public DbSet<Caller> Callers { get; set; }
        public DbSet<RecievedCall> RecievedCalls { get; set; }
        public DbSet<Department> Departments { get; set; }


        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<RingStiffness> RingStiffnesses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PipeDiameter> PipeDiameters { get; set; }
        public DbSet<PipeProfile> PipeProfiles { get; set; }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<InquiryItem> InquiryItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<TenderItem> TenderItems { get; set; }
        public DbSet<Rival> Rivals { get; set; }
        public DbSet<RivalPrice> RivalPrices { get; set; }

        public DbSet<ActivityLog> ActivityLogs { get; set; }





        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.CallerMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.RecievedCallMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.DepartmentMapping());

                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.CustomerMapping());

                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.ProductCategoryMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.RingStiffnessMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.PipeDiameterMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.PipeProfileMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.ProductMapping());

                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.InquiryItemMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.InquiryMapping());

                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.OrderItemMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.OrderMapping());

                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.RivalMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.RivalPriceMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.TenderItemMapping());
                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.TenderMapping());

                modelBuilder.Configurations.Add(new Neshagostar.DAL.FluentAPI.ActivityLogMapping());

          
            }
            catch
            {
                throw;
            }
            base.OnModelCreating(modelBuilder);
        }






    }
}
