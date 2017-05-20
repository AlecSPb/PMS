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
        [OperationContract]
        List<DcPMSIndex> GetPMSIndexByType(string pmsIndexType);

        [OperationContract]
        DcPMSIndex GetBestInHistory(string pmsIndexType);

        [OperationContract]
        DcPMSIndex GetWorstInHistory(string pmsIndexType);

        [OperationContract]
        DcPMSIndex GetAverageInHistory(string pmsIndexType);


        /// <summary>
        /// 每天计算一下
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [OperationContract]
        int AddPMSIndex(DcPMSIndex index);

    }
}
