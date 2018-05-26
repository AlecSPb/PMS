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


        public DbSet<MaterialNeedHistory> MaterialNeedHistorys { get; set; }
        public DbSet<MaterialOrderHistory> MaterialOrderHistorys { get; set; }
        public DbSet<MaterialOrderItemHistory> MaterialOrderItemHistorys { get; set; }

        public DbSet<MaterialInventoryInHistory> MaterialInventoryInHistorys { get; set; }
        public DbSet<MaterialInventoryOutHistory> MaterialInventoryOutHistorys { get; set; }

        public DbSet<Plate> Plates { get; set; }
        public DbSet<PlateHistory> PlateHistorys { get; set; }


        //Core
        public DbSet<PMSOrder> Orders { get; set; }
        public DbSet<PMSPlanVHP> VHPPlans { get; set; }

        public DbSet<PMSOrderHistory> OrderHistorys { get; set; }
        public DbSet<PMSPlanVHPHistory> VHPPlanHistorys { get; set; }

        public DbSet<OutSource> OutSources { get; set; }
        public DbSet<OutSourceHistory> OutSourceHistorys { get; set; }
        //Records
        public DbSet<RecordMilling> RecordMillings { get; set; }
        public DbSet<RecordVHP> RecordVHPs { get; set; }
        public DbSet<RecordDeMold> RecordDeMolds { get; set; }
        public DbSet<RecordMachine> RecordMachines { get; set; }
        public DbSet<RecordTest> RecordTests { get; set; }
        public DbSet<RecordBonding> RecordBondings { get; set; }


        public DbSet<RecordMillingHistory> RecordMillingHistorys { get; set; }
        public DbSet<RecordVHPHistory> RecordVHPHistorys { get; set; }
        public DbSet<RecordDeMoldHistory> RecordDeMoldHistorys { get; set; }
        public DbSet<RecordMachineHistory> RecordMachineHistorys { get; set; }
        public DbSet<RecordTestHistory> RecordTestHistorys { get; set; }
        public DbSet<RecordBondingHistory> RecordBondingHistorys { get; set; }
        //Product
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductHistory> ProductHistorys { get; set; }
        //Delivery
        public DbSet<Delivery> Deliverys { get; set; }
        public DbSet<DeliveryItem> DeliveryItems { get; set; }

        public DbSet<DeliveryHistory> DeliveryHistorys { get; set; }
        public DbSet<DeliveryItemHistory> DeliveryItemHistorys { get; set; }
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

        //Operate
        public DbSet<CheckList> CheckLists { get; set; }
        public DbSet<CheckListHistory> CheckListHistorys { get; set; }

        public DbSet<ItemDebit> ItemDebits { get; set; }
        public DbSet<ItemDebitHistory> ItemDebitHistorys { get; set; }

        public DbSet<FeedBack> FeedBacks { get; set; }


        //PMSIndex
        public DbSet<PMSIndex> PMSIndexs { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<TaskPlan> TaskPlans { get; set; }

        public DbSet<EnvironmentInfo> EnvironmentInfos { get; set; }

        public DbSet<Notice> Notices { get; set; }

        public DbSet<ToDo> ToDoes { get; set; }

        //Tool Inventory
        public DbSet<ToolMilling> ToolMillings { get; set; }
        public DbSet<ToolFilling> ToolFillings { get; set; }
    }
}
