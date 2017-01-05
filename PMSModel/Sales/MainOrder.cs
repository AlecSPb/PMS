using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSModel
{
    /// <summary>
    /// 订单
    /// </summary>
    public class MainOrder
    {
        //基本信息
        public Guid ID { get; set; }
        public string CustomerName { get; set; }
        public string PO { get; set; }
        public string PMIWorkingNumber { get; set; }

        public string CompositionStandard { get; set; }//成分规范表示
        public string CompositionOriginal { get; set; }//成分原始表示
        public string CompositoinAbbr { get; set; }
        public string ProductType { get; set; }
        public string Purity { get; set; }

        public double Quantity { get; set; }
        public string QuantityUnit { get; set; }

        public string Dimension { get; set; }//尺寸标准表示
        public string DimensionNeed { get; set; }//尺寸细节
        public string SampleNeed { get; set; }//是否需要样品
        public DateTime ScheduleDeliveryDate { get; set; }

        public string DefectAccept { get; set; }//密度要求，加工要求，表面洁净度，多大的缺口可以接受，多大的裂缝可以接受，表面有花纹是否可以接受，表面有坑是否可以接受

        //状态部分
        public int Priority { get; set; }//紧急，一般，不着急
        public int CurrentState { get; set; }//正在生产，生产完成，发货完成，取消，暂停
        public string CurrentStateReason { get; set; }//当前状态的原因，主要给取消和暂停用


        //创建者和审核部分
        public DateTime CreateTime { get; set; }
        public string Creator { get; set; }

        public bool ReviewPassed { get; set; }//审核是否通过
        public string Reviewer { get; set; }
        public DateTime ReviewDate { get; set; }

        //决策部分
        public string PolicyType { get; set; }//热压，代工，库存，其他,只有热压订单才会进入任务
        public string PolicyContent { get; set; }//决策内容
        public string PolicyMaker { get; set; }//决策者
        public DateTime PolicyMakeDate { get; set; }


    }
}
