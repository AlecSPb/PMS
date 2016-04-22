using System;

namespace WcfService.Model
{
    public class MainPlan
    {
        public Guid MainPlanId { get; set; }

        public Guid MainOrderId { get; set; }

        public DateTime? VHPTime { get; set; }

        public string VHPType { get; set; }

        public string DeviceType { get; set; }

        public string ProcessCode { get; set; }

        public double MoldDiameter { get; set; }

        public double Thickness { get; set; }

        public int VHPQuantity { get; set; }

        public string Pressure { get; set; }

        public string Temperature { get; set; }

        public string Vaccum { get; set; }

        public double DensityCal { get; set; }

        public string PersonInCharge { get; set; }

        public string Remark { get; set; }
    }
}