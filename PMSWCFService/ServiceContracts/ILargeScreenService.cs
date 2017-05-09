using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface ILargeScreenService
    {
        [OperationContract]
        List<DcPlanExtra> GetPlanByDate(DateTime planDate);
        [OperationContract]
        List<DcStatistic> GetPlanStatistic();

        [OperationContract]
        List<DcRecordBonding> GetBondingUnComplete(int s,int t);
        [OperationContract]
        int GetBondingUnCompleteCount();
        [OperationContract]
        List<DcStatistic> GetBondingCompleteStatistic();

    }
}
