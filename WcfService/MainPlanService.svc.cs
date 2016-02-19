using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“MainPlanService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 MainPlanService.svc 或 MainPlanService.svc.cs，然后开始调试。
    public class MainPlanService : IMainPlanService
    {
        public bool AddMainPlan(MainPlan plan)
        {
            throw new NotImplementedException();
        }

        public bool DeleteMainPlan(MainPlan plan)
        {
            throw new NotImplementedException();
        }

        public void GetMainPlansByMainOrderId()
        {
        }

        public List<MainPlan> GetMainPlansByMainOrderId(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMainPlan(MainPlan plan)
        {
            throw new NotImplementedException();
        }
    }
}
