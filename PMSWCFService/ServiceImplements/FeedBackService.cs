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
            try
            {
                using (var dc=new PMSDbContext())
                {

                }

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteFeedBack(Guid id, string uid)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcFeedBack> GetFeedBack(int s, int t, string productId, string composition, string customer)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetFeedBackCount(string productId, string composition, string customer)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateFeedBack(DcFeedBack model, string uid)
        {
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
    }
}