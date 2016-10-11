using PMSDAL.Model;
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
        public PMSDbContext() : base("PMSConStr")
        {

        }

        public DbSet<AppAccess> AppAccess { get; set; }
        public DbSet<AppRole> AppRole { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<CalculationCondition> CalculationCondition { get; set; }
        public DbSet<CalculationConditionItem> CalculationConditionItem { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<MainOrder> MainOrder { get; set; }
        public DbSet<MainPlan> MainPlan { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Mold> Mold { get; set; }
        public DbSet<RecordPowder> RecordPowder { get; set; }
        public DbSet<RecordVHP> RecordVHP { get; set; }
        public DbSet<Sample> Sample { get; set; }
        public DbSet<Target> Target { get; set; }
        public DbSet<VHPDevice> VHPDevice { get; set; }



    }
}
