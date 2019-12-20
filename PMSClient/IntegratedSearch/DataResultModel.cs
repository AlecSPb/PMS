using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.IntegratedSearch
{
    /// <summary>
    /// 用于存储查询结果
    /// </summary>
    public class DataResultModel
    {
        public DataResultModel()
        {
            IsSucceed = false;
            Result = new StringBuilder();
            SearchProductID = SearchPMINumber = "";
        }
        public bool IsSucceed { get; set; }
        public StringBuilder Result { get; set; }
        public string SearchProductID { get; set; }
        public string SearchPMINumber { get; set; }
        public Guid SearchOrderId { get; set; }
        public Guid SearchPlanId { get; set; }
    }
}
