using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“LogDataService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 LogDataService.svc 或 LogDataService.svc.cs，然后开始调试。
    public class LogDataService : ILogDataService
    {
        public bool AddLogData(LogData data)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllLogData()
        {
            throw new NotImplementedException();
        }

        public bool DeleteLogDataById(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<LogData> GetAllLogData()
        {
            throw new NotImplementedException();
        }
    }
}
