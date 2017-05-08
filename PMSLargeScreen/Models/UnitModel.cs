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
            DeviceCode = "A";
            PlanLot = 1;
            MoldInnerDiameter = 230;
            MoldType = "高强";
            Pressure = 65;
            Temp = 350;
            Vaccum = 0.001;
            KeepTime = 120;
            Items = new List<UnitModelItem>()
            {
                new UnitModelItem {Composition="Cu22.8In20Ga7Se50.2",Quantity=1,SingleWeight=1200,ProcessCode="W1",PlanType = "加工",FillRequirement="BN+石墨纸+Al2O3",PMINumber="" },
                new UnitModelItem {Composition="Cu22.8In21Ga6Se50.2",Quantity=2,SingleWeight=1300 ,ProcessCode="W1",PlanType = "加工",FillRequirement="BN+石墨纸+Al2O3" ,PMINumber=""},
                new UnitModelItem {Composition="Cu22.8In22Ga5Se50.2",Quantity=3,SingleWeight=1400,ProcessCode="W1",PlanType = "加工",FillRequirement="BN+石墨纸+Al2O3" ,PMINumber="" }
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
    }

}
