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
    public interface IProcessCodeService
    {
        /// <summary>
        /// 得到所有的工艺代码
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<ProcessCode> GetAllProcessCodes();

        [OperationContract]
        bool AddProcessCode(ProcessCode code);
        [OperationContract]
        bool UpdateProcessCode(ProcessCode code);
        [OperationContract]
        bool DeleteProcessCode(ProcessCode code);

    }
}
