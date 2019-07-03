using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSLargeScreen.Models
{
    public class UnitModel
    {
        public UnitModel()
        {
            DeviceCode = "空";
            PlanLot = 0;
            MoldInnerDiameter = 0;
            MoldType = "无";
            Pressure = 0;
            Temp = 0;
            Vaccum = 0;
            KeepTime = 0;
            IsLocked = false;
            Items = new List<UnitModelItem>()
            {
            };
        }

        public string DeviceCode { get; set; }
        public int PlanLot { get; set; }
        public double MoldInnerDiameter { get; set; }
        public string MoldType { get; set; }
        public double Pressure { get; set; }
        public double Temp { get; set; }
        public double Vaccum { get; set; }
        public double KeepTime { get; set; }
        public bool IsLocked { get; set; }
        public List<UnitModelItem> Items { get; set; }
    }
    public class UnitModelItem
    {
        public string Composition { get; set; }
        public string PMINumber { get; set; }
        public double SingleWeight { get; set; }
        public int Quantity { get; set; }
        public string ProcessCode { get; set; }
        public string PlanType { get; set; }
        public string FillRequirement { get; set; }
        public string VHPRequirement { get; set; }
    }

}
