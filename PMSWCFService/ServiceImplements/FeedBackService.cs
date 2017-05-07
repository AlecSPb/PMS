using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;

namespace PMSWCFService
{
    public partial class PMSService : IFeedBackService
    {
        public int AddFeedBack(DcFeedBack model, string uid)
        {
            throw new NotImplementedException();
        }

        public int DeleteFeedBack(Guid id, string uid)
        {
            throw new NotImplementedException();
        }

        public List<DcFeedBack> GetFeedBack(int s, int t, string productId, string composition, string customer)
        {
            throw new NotImplementedException();
        }

        public int GetFeedBackCount(string productId, string composition, string customer)
        {
            throw new NotImplementedException();
        }

        public int UpdateFeedBack(DcFeedBack model, string uid)
        {
            throw new NotImplementedException();
        }
    }
}