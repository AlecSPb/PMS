using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ILogDataService”。
    [ServiceContract]
    public interface ILogDataService
    {
        [OperationContract]
        List<LogData> GetAllLogData();
        [OperationContract]
        bool AddLogData(LogData data);
        [OperationContract]
        bool DeleteAllLogData();
        [OperationContract]
        bool DeleteLogDataById(Guid id);
    }
}
