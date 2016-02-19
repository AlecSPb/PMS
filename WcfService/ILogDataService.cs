using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfService.Model;

namespace WcfService
{
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
