using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 订单
    /// </summary>
    public class Order
    {
        public Guid ID { get; set; }
        public string CustomerName { get; set; }
        public string PO { get; set; }
        public string PMIWorkingNumber { get; set; }

        public string Composition { get; set; }//成分规范表示
        public string CompositionOriginal { get; set; }//成分原始表示

        public string ProductType { get; set; }
        public string Purity { get; set; }

        public double Quantity { get; set; }
        public string QuantityUnit { get; set; }

        public string Dimension { get; set; }//尺寸标准表示
        public string DimensionRequirement { get; set; }//尺寸细节
        public string SampleRequirement { get; set; }//是否需要样品
        public DateTime DeliveryDate { get; set; }

        //状态部分
        public int Priority { get; set; }//紧急，一般，不着急
        public int CurrentState { get; set; }//正在生产，生产完成，发货完成，取消，暂停
        public string CurrentStateReason { get; set; }//当前状态的原因，主要给取消和暂停用


        //创建和审核部分
        public DateTime CreateDate { get; set; }
        public string Creator { get; set; }

        public bool ReviewPassed { get; set; }//审核是否通过
        public string Reviewer { get; set; }
        public DateTime ReviewDate { get; set; }

        //决策部分
        public string PolicyType { get; set; }
        public string PolicyContent { get; set; }
        public string PolicyMaker { get; set; }
        public DateTime PolicyMakeDate { get; set; }


    }
}
