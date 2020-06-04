using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSEOrder.Model
{
    [Table("localorder")]
    public class Order
    {
        //基本信息
        [Key]
        public int ID { get; set; }//丘这里就生成GUID,PMS系统直接录入该ID ，作为唯一核对订单的标识
        public Guid GUIDID { get; set; }
        public string CustomerName { get; set; }
        public string PO { get; set; }
        public string Composition { get; set; }
        public string CompositionDetail { get; set; }
        public string ProductType { get; set; }
        public string Purity { get; set; }
        public double Quantity { get; set; }
        public string QuantityUnit { get; set; }
        public string Dimension { get; set; }
        public string DimensionDetails { get; set; }
        public string Drawing { get; set; }//图纸
        public string SampleNeed { get; set; }
        public string SampleNeedRemark { get; set; }//是否需要样品备注
        public string SampleForAnlysis { get; set; }//PMI是否需要取样分析
        public string SampleForAnlysisRemark { get; set; }//PMI是否需要取样分析备注
        public DateTime DeadLine { get; set; }
        public string MinimumAcceptDefect { get; set; }
        public string ShipTo { get; set; }//发货目的地
        public string WithBackingPlate { get; set; }//是否配对应背板
        public string PlateDrawing { get; set; }//背板图纸
        public string SpecialRequirement { get; set; }
        public string BondingRequirement { get; set; }
        public string PartNumber { get; set; }
        public string Remark { get; set; }
        public DateTime CreateTime { get; set; }
        public string OrderState { get; set; }//作废，未填完，未核对，未发送，已发送，已接受
        public string Creator { get; set; }

    }
}
