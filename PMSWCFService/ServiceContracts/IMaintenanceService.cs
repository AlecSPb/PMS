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
        List<DcMaintenancePlan> GetMaintenancePlans(int skip, int take);
        [OperationContract]
        int GetMaintenancePlanCount();
        [OperationContract]
        int AddMainitenancePlan(DcMaintenancePlan model);
        [OperationContract]
        int UpdateMainitenancePlan(DcMaintenancePlan model);
        [OperationContract]
        int DeleteMainitenancePlan(Guid id);



        [OperationContract]
        List<DcMaintenanceRecord> GetMaintenanceRecords(Guid planid);
        [OperationContract]
        int AddMainitenanceRecord(DcMaintenanceRecord model);
        [OperationContract]
        int UpdateMainitenanceRecord(DcMaintenanceRecord model);
        [OperationContract]
        int DeleteMainitenanceRecord(Guid id);


    }
}
