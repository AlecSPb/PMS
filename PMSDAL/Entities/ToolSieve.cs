using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL.Entities
{
    /// <summary>
    /// 筛网库
    /// </summary>
    public class ToolSieve:ModelBase
    {
        public string SearchID { get; set; }//给人看的编号  S-1
        public string Manufacture { get; set; }//制造商
        public string Specification { get;; set; }//规格
        public string MaterialGroup { get; set; }//材料组
        public string Remark { get; set; }

        public DateTime StartTime { get; set; }//开始使用时间
        public DateTime StopTime { get; set; }//结束使用时间

        //State:CommonToolState
    }
}
