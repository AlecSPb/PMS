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
    public interface IMoldService
    {
        /// <summary>
        /// 得到所有的模具
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Mold> GetAllMolds();

        [OperationContract]
        bool AddMold(Mold mold);
        [OperationContract]
        bool UpdateMold(Mold mold);
        [OperationContract]
        bool DeleteMold(Mold mold);

    }
}
