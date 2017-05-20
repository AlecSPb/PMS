using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;

namespace PMSWCFService
{
    public class PMSIndexService : IPMSIndexService
    {
        public int AddPMSIndex(DcPMSIndex index)
        {
            throw new NotImplementedException();
        }

        public DcPMSIndex GetAverageInHistory(string pmsIndexType)
        {
            throw new NotImplementedException();
        }

        public DcPMSIndex GetBestInHistory(string pmsIndexType)
        {
            throw new NotImplementedException();
        }

        public List<DcPMSIndex> GetPMSIndexByType(string pmsIndexType)
        {
            throw new NotImplementedException();
        }

        public DcPMSIndex GetWorstInHistory(string pmsIndexType)
        {
            throw new NotImplementedException();
        }
    }
}