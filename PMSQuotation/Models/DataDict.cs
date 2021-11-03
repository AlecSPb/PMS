using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSQuotation.Models
{
    /// <summary>
    /// 基本数据字典
    /// </summary>
    public class DataDict
    {
        public int ID { get; set; }
        public string DataKey { get; set; }
        public string DataValue { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string State { get; set; }
    }
}
