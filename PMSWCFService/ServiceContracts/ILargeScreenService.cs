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
        //热压计划
        [OperationContract]
        List<DcPlanExtra> GetPlanByDate(DateTime planDate);
        [OperationContract]
        List<DcPlanExtra> GetPlanByDateDeviceCode(int planlot,DateTime planDate, string deviceCode);
        [OperationContract]
        List<DcStatistic> GetPlanStatistic();

        //绑定计划
        [OperationContract]
        List<DcRecordBonding> GetBondingUnComplete(int s,int t);
        [OperationContract]
        int GetBondingUnCompleteCount();
        [OperationContract]
        List<DcStatistic> GetBondingCompleteStatistic();



        //制粉计划
        [OperationContract]
        List<DcRecordMilling> GetRecordMillings(DateTime planDate);


    }
}
