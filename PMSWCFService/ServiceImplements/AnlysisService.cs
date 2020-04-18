using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;

namespace PMSWCFService
{
    /// <summary>
    /// 数据分析服务
    /// </summary>
    public class AnlysisService : IAnlysisService
    {
        public List<DcPlanTrace> GetPlanTrace(int s, int t, string plan, string composition, string pminumber)
        {
            throw new NotImplementedException();
        }

        public int GetPlanTraceCount(string plan, string composition, string pminumber)
        {
            throw new NotImplementedException();
        }
    }
}