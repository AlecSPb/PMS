using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;

namespace PMSWCFService
{
    public partial class PMSService : IStatisticService
    {
        public DcStatisticDelivery GetStatisticDelivery()
        {
            throw new NotImplementedException();
        }

        public DcStatisticOrder GetStatisticOrder()
        {
            throw new NotImplementedException();
        }

        public DcStatisticPlan GetStatisticPlan()
        {
            throw new NotImplementedException();
        }

        public DcStatisticProduct GetStatisticProduct()
        {
            throw new NotImplementedException();
        }
    }
}