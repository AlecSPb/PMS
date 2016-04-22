using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DataAccessLibrary.Model
{
    public class MainOrder
    {
        [Key]
        public Guid MainOrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public string CustomerName { get; set; }

        public string ProductName { get; set; }

        public string PO { get; set; }

        public string PMIWorkNumber { get; set; }

        public string ProductType { get; set; }

        public string Purity { get; set; }

        public string Shape { get; set; }

        public string Dimension { get; set; }

        public double Quantity { get; set; }

        public string Unit { get; set; }

        public int Priority { get; set; }

        public string SampleRequirement { get; set; }

        public string OrderState { get; set; }

        public DateTime? DeliveryDateExpect { get; set; }

        public string Consignee { get; set; }

        public bool? IsPlanFinished { get; set; }

        public bool? IsDeliveryFinished { get; set; }

        public DateTime? DeliveryDateFact { get; set; }

        public string Remark { get; set; }

        public virtual List<MainPlan> MainPlans { get; set; }

    }
}
