using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSEOrder.Model
{
    public class Order
    {
        //基本信息
        
        public Guid ID { get; set; }      
        public string CustomerName { get; set; }    
        public string PO { get; set; }
        public string PMINumber { get; set; }
        public string CompositionStandard { get; set; }
        public string CompositionOriginal { get; set; }
        public string CompositionAbbr { get; set; }
        public string ProductType { get; set; }
        public string Purity { get; set; }
        public double Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public string Dimension { get; set; }
        public string DimensionDetails { get; set; }
        public string SampleNeed { get; set; }
        public DateTime DeadLine { get; set; }
        public string MinimumAcceptDefect { get; set; }
        public string Remark { get; set; }
        public string Drawing { get; set; }//图纸
        public string SampleForAnlysis { get; set; }//PMI是否需要取样分析
        public string ShipTo { get; set; }//发货目的地
        public string WithBackingPlate { get; set; }//是否配对应背板
        public string PlateDrawing { get; set; }//背板图纸
        public string SpecialRequirement { get; set; }

        public DateTime CreateTime { get; set; }
        public string PartNumber { get; set; }
        public string SecondMachineDimension { get; set; }
        public string SecondMachineDetails { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string SampleNeedRemark { get; set; }//是否需要样品备注
        public string SampleForAnlysisRemark { get; set; }//PMI是否需要取样分析备注
    }
}
