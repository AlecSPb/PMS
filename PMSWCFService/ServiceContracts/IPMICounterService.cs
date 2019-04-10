using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IPMICounterService
    {
        [OperationContract]
        List<DcPMICounter> GetPMICounter(string itemGroup,string itemName, int s, int t);
        [OperationContract]
        int GetPMICounterCount(string itemGroup, string itemName);

        [OperationContract]
        int AddPMICounter(DcPMICounter model);
        [OperationContract]
        int UpdatePMICounter(DcPMICounter model);
        [OperationContract]
        int DeletePMICounter(Guid id);

    }
}