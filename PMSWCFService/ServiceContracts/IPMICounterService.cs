using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;

namespace PMSWCFService
{
    public interface IPMICounterService
    {
        List<DcPMICounter> GetPMICounter(string itemGroup,string itemName, int s, int t);
        int GetPMICounterCount(string itemGroup, string itemName);

        int AddPMICounter(DcPMICounter model);
        int UpdatePMICounter(DcPMICounter model);
        int DeletePMICounter(Guid id);

    }
}