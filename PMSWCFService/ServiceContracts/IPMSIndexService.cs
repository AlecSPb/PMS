using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;
using PMSDAL;
using AutoMapper;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IPMSIndexService
    {
        #region 热压指数计算
        [OperationContract]
        void CalculateProductionIndex(Guid orderid);
        [OperationContract]
        void CalculateMaterialIndex(Guid orderid);
        #endregion

    }
}
