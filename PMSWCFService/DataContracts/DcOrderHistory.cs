using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PMSWCFService.DataContracts
{
    /// <summary>
    /// 订单
    /// </summary>
    [DataContract]
    public class DcOrderHistory
    {
        //基本信息
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public string PO { get; set; }
        [DataMember]
        public string PMINumber { get; set; }

        [DataMember]
        public string CompositionStandard { get; set; }//成分规范表示
        [DataMember]
        public string CompositionOriginal { get; set; }//成分原始表示
        [DataMember]
        public string CompositionAbbr { get; set; }
        [DataMember]
        public string ProductType { get; set; }
        [DataMember]
        public string Purity { get; set; }

        [DataMember]
        public double Quantity { get; set; }
        [DataMember]
        public string QuantityUnit { get; set; }

        [DataMember]
        public string Dimension { get; set; }//尺寸标准表示
        [DataMember]
        public string DimensionDetails { get; set; }//尺寸细节
        [DataMember]
        public string SampleNeed { get; set; }//是否需要样品
        [DataMember]
        public DateTime DeadLine { get; set; }

        [DataMember]
        public string MinimumAcceptDefect { get; set; }//密度要求，加工要求，表面洁净度，多大的缺口可以接受，多大的裂缝可以接受，表面有花纹是否可以接受，表面有坑是否可以接受
        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string Drawing { get; set; }//图纸
        [DataMember]
        public string SampleForAnlysis { get; set; }//PMI是否需要取样分析
        [DataMember]
        public string ShipTo { get; set; }//发货目的地
        [DataMember]
        public string WithBackingPlate { get; set; }//是否配对应背板        
        [DataMember]
        public string PlateDrawing { get; set; }//背板图纸
        [DataMember]
        public string SpecialRequirement { get; set; }
        //热压指数
        [DataMember]
        public double ProductionIndex { get; set; }
        [DataMember]
        public double MaterialIndex { get; set; }

        //状态部分
        [DataMember]
        public string Priority { get; set; }//紧急，一般，不着急
        [DataMember]
        public string State { get; set; }//正在生产，生产完成，发货完成，取消，暂停
        [DataMember]
        public string StateRemark { get; set; }//当前状态的原因，主要给取消和暂停用


        //创建者和审核部分
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime ReviewTime { get; set; }
        [DataMember]
        public string Reviewer { get; set; }
        //决策部分
        [DataMember]
        public string PolicyType { get; set; }//热压，代工，库存，其他,只有热压订单才会进入任务
        [DataMember]
        public DateTime FinishTime { get; set; }
        //操作者和操作时间
        [DataMember]
        public Guid HistoryID { get; set; }
        [DataMember]
        public string Operator { get; set; }
        [DataMember]
        public DateTime OperateTime { get; set; }

        [DataMember]
        public string PartNumber { get; set; }
        [DataMember]
        public string SecondMachineDimension { get; set; }
        [DataMember]
        public string SecondMachineDetails { get; set; }
        [DataMember]
        public DateTime LastUpdateTime { get; set; }

        [DataMember]
        public string SampleNeedRemark { get; set; }//是否需要样品备注
        [DataMember]
        public string SampleForAnlysisRemark { get; set; }//PMI是否需要取样分析备注
        [DataMember]
        public string OrderRemark { get; set; }

        [DataMember]
        public string BondingRequirement { get; set; }
    }
}
