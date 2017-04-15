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
        public DbSet<BDCompound> Compounds { get; set; }
        public DbSet<BDCustomer> Customers { get; set; }
        public DbSet<BDElement> Elements { get; set; }
        public DbSet<BDDeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<BDSieve> Sieves { get; set; }
        public DbSet<BDSupplier> Suppliers { get; set; }
        public DbSet<BDVHPDevice> VHPDevices { get; set; }
        public DbSet<BDVHPMold> VHPMolds { get; set; }
        public DbSet<BDVHPProcess> VHPProcesses { get; set; }

        public DbSet<BDElementGroup> ElementGroups { get; set; }
        public DbSet<BDElementGroupItem> ElementGroupItems { get; set; }

        //Material
        public DbSet<MaterialNeed> MaterialNeeds { get; set; }
        public DbSet<MaterialOrder> MaterialOrders { get; set; }
        public DbSet<MaterialOrderItem> MaterialOrderItems { get; set; }

        public DbSet<MaterialInventoryIn> MaterialInventoryIns { get; set; }
        public DbSet<MaterialInventoryOut> MaterialInventoryOuts { get; set; }
        //Core
        public DbSet<PMSOrder> Orders { get; set; }
        public DbSet<PMSPlanVHP> VHPPlans { get; set; }

        //Records
        public DbSet<RecordMachine> RecordMachines { get; set; }
        public DbSet<RecordMilling> RecordMillings { get; set; }
        public DbSet<RecordDeMold> RecordDeMolds { get; set; }
        public DbSet<RecordVHP> RecordVHPs { get; set; }
        public DbSet<RecordTest> RecordTests { get; set; }
        public DbSet<RecordBonding> RecordBondings { get; set; }

        //Product
        public DbSet<Product> Products { get; set; }
        //Delivery
        public DbSet<Delivery> Deliverys { get; set; }
        public DbSet<DeliveryItem> DeliveryItems { get; set; }


        //Maintenance
        public DbSet<MaintenancePlan> MaintenancePlans { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }

        //UserAccess
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<UserAccess> Accesses { get; set; }

        //Log
        public DbSet<LogError> LogErrors { get; set; }
        public DbSet<LogInformation> LogInformations { get; set; }

    }
}
