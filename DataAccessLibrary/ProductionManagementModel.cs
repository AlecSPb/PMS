namespace DataAccessLibrary
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProductionManagementModel : DbContext
    {
        public ProductionManagementModel()
            : base("name=ProductionManagementModel")
        {
        }

        public virtual DbSet<V_MainOrder> V_MainOrder { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
