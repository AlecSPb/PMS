using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PMSDAL
{
    public class PMSDbContext : DbContext
    {
        public PMSDbContext() : base("name=Default")
        {
            //数据库初始化器
            //Database.SetInitializer<PMSDbContext>(null);
        }
        //BasicData
        public DbSet<Compound> Comounds { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<VHPDevice> VHPDevices { get; set; }
        public DbSet<VHPMold> VHPMolds { get; set; }
        public DbSet<VHPProcess> VHPProcesses { get; set; }

        //Material
        public DbSet<PMSMaterialNeed> MaterialNeeds { get; set; }
        public DbSet<PMSMaterialOrder> MaterialOrders { get; set; }
        public DbSet<PMSMaterialOrderItem> MaterialOrderItems { get; set; }

        //Sales
        public DbSet<PMSOrder> Orders { get; set; }
        //Production
        public DbSet<PMSPlanVHP> VHPPlans { get; set; }
        //Product
        public DbSet<PMSProduct> Products { get; set; }
        public DbSet<PMSSample> Samples { get; set; }

        //Delivery
        public DbSet<Delivery> Deliverys { get; set; }
        public DbSet<DeliveryItem> DeliveryItems { get; set; }

        //Record
        //public DbSet<RecordBonding>  RecordBondings { get; set; }
        public DbSet<RecordMachine> RecordMachines { get; set; }
        public DbSet<RecordMilling> RecordMillings { get; set; }
        public DbSet<RecordTakeOut> RecordTakeOuts { get; set; }
        public DbSet<RecordVHP> RecordVHPs { get; set; }
        public DbSet<RecordVHPItem> RecordVHPItems { get; set; }

        //UserAccess
        public DbSet<PMSUser> Users { get; set; }
        public DbSet<PMSRole> Roles { get; set; }
        public DbSet<PMSAccess> Accesses { get; set; }

    }
}
