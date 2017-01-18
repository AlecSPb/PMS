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
        public DbSet<BDCompound> Comounds { get; set; }
        public DbSet<BDCustomer> Customers { get; set; }
        public DbSet<BDDeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<BDVHPDevice> VHPDevices { get; set; }
        public DbSet<BDVHPMold> VHPMolds { get; set; }
        public DbSet<BDVHPProcess> VHPProcesses { get; set; }

        //Material
        public DbSet<PMSMaterialNeed> MaterialNeeds { get; set; }
        public DbSet<PMSMaterialOrder> MaterialOrders { get; set; }
        public DbSet<PMSMaterialOrderItem> MaterialOrderItems { get; set; }

        public DbSet<PMSOrder> Orders { get; set; }
        public DbSet<PMSPlanVHP> VHPPlans { get; set; }

        public DbSet<RecordDelivery> Deliverys { get; set; }
        public DbSet<RecordDeliveryItem> DeliveryItems { get; set; }
        public DbSet<RecordMachine> RecordMachines { get; set; }
        public DbSet<RecordMilling> RecordMillings { get; set; }
        public DbSet<RecordTakeOut> RecordTakeOuts { get; set; }
        public DbSet<RecordVHP> RecordVHPs { get; set; }
        public DbSet<RecordVHPItem> RecordVHPItems { get; set; }
        public DbSet<RecordProduct> Products { get; set; }
        //UserAccess
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<UserAccess> Accesses { get; set; }

    }
}
