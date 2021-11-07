using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    /// <summary>
    /// 用于json文件导入导出
    /// </summary>
    public class QuotationJsonModel
    {
        public Quotation Quotation { get; set; }
        public List<QuotationItem> QuotationItems { get; set; }
    }
}
