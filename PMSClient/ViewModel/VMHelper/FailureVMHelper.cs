using PMSClient.FailureService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel.VMHelper
{
    public class FailureVMHelper
    {
        public static DcFailure GetNewFailure()
        {
            var model = new DcFailure();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.ProductID = "无";
            model.Composition = "无";
            model.Details = "无";
            model.Stage = "";
            model.Problem = "无";
            model.Process = "未处理";
            model.Remark = "无";
            return model;
        }
    }
}
