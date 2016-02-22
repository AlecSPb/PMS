using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DataAccessLibrary.Model;
namespace DataAccessLibrary
{
    public  class ProductionManagementModel : DbContext
    {
        public ProductionManagementModel()
            : base("name=ProductionManagementModel")
        {
        }

        public virtual DbSet<MainOrder> MainOrders { get; set; }
        public DbSet<MainPlan> MainPlans { get; set; }
        public DbSet<Device> Devices { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
