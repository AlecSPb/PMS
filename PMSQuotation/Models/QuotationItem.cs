using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    /// <summary>
    /// 报价项目
    /// 以靶材为项目
    /// </summary>
    public class QuotationItem
    {
        public Guid ID { get; set; }
        public Guid QuotationID { get; set; }

        public string Composition { get; set; }
        public string Dimension { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string DeliveryDate { get; set; }
        public bool WithTax { get; set; }
        public string Note { get; set; }

        /// <summary>
        /// 存储计算过程
        /// 原材料
        /// 制粉
        /// 热压
        /// 加工
        /// 绑定
        /// 测试
        /// 包装
        /// 运输
        /// 关税
        /// 增值税
        /// </summary>
        public string CalcualtionJson { get; set; }

    }
}
