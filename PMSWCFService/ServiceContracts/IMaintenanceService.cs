using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;
using PMSDAL;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IMaintenanceService
    {
        [OperationContract]
        List<DcMaintenancePlan> GetMaintenancePlans(int s, int t,string devicecode,string planitem);
        [OperationContract]
        int GetMaintenancePlanCount(string devicecode, string planitem);
        [OperationContract]
        int AddMainitenancePlan(DcMaintenancePlan model);
        [OperationContract]
        int UpdateMainitenancePlan(DcMaintenancePlan model);


        [OperationContract]
        List<DcMaintenanceRecord> GetMaintenanceRecords(int s, int t,string device,string planitem);
        [OperationContract]
        int GetMaintenanceRecordsCount(string device, string planitem);

        [OperationContract]
        int AddMainitenanceRecord(DcMaintenanceRecord model);
        [OperationContract]
        int UpdateMainitenanceRecord(DcMaintenanceRecord model);

    }
}
