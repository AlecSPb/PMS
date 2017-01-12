using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using AutoMapper;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IPlanVHPService
    {
        [OperationContract]
        List<DcPlanVHP> GetVHPPlansByOrderID(Guid id);
        [OperationContract]
        int AddVHPPlan(DcPlanVHP model);
        [OperationContract]
        int UpdateVHPPlan(DcPlanVHP model);
        [OperationContract]
        int DeleteVHPPlan(Guid id);
    }
}