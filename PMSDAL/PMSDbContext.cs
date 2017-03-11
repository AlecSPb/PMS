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
        public DbSet<BDDeliveryAddress> DeliveryAddresses { get; set; }
        public DbSet<BDVHPDevice> VHPDevices { get; set; }
        public DbSet<BDVHPMold> VHPMolds { get; set; }
        public DbSet<BDVHPProcess> VHPProcesses { get; set; }
        public DbSet<BDSupplier> Suppliers { get; set; }

        //Material
        public DbSet<PMSMaterialNeed> MaterialNeeds { get; set; }
        public DbSet<PMSMaterialOrder> MaterialOrders { get; set; }
        public DbSet<PMSMaterialOrderItem> MaterialOrderItems { get; set; }

        //Core
        public DbSet<PMSOrder> Orders { get; set; }
        public DbSet<PMSPlanVHP> VHPPlans { get; set; }

        //Records
        public DbSet<RecordDelivery> RecordDeliverys { get; set; }
        public DbSet<RecordDeliveryItem> RecordDeliveryItems { get; set; }
        public DbSet<RecordMachine> RecordMachines { get; set; }
        public DbSet<RecordMilling> RecordMillings { get; set; }
        public DbSet<RecordDeMold> RecordDeMolds { get; set; }
        public DbSet<RecordVHP> RecordVHPs { get; set; }
        public DbSet<RecordTestResult> RecordTestResults { get; set; }

        public DbSet<RecordBonding> RecordBondings { get; set; }
        public DbSet<RecordBondingPlate> RecordBondingPlates { get; set; }
        public DbSet<RecordBondingTarget> RecordBondingTargets { get; set; }
        //UserAccess
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<UserAccess> Accesses { get; set; }

        //Maintenance
        public DbSet<MaintenancePlan> MaintenancePlans { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }

    }
}
