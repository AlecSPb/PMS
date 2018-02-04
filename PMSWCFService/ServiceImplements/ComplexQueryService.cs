using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using PMSWCFService.DataContracts;

namespace PMSWCFService
{
    public class ComplexQueryService :IComplexQuery
    {
        public List<DcComplexQueryResult> GetAllStatusByPlanNumber(string planNumber)
        {
            DcComplexQueryResult result = new DcComplexQueryResult();
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
            }
            return null;
        }

        public List<DcComplexQueryResult> GetAllStatusByPMINumber(string pmiNumber)
        {
            DcComplexQueryResult result = new DcComplexQueryResult();
            try
            {

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
            }
            return null;
        }

        public Dictionary<string, bool> GetCurrentOrderStatus(string pmiNumber,string p)
        {
            Dictionary<string, bool> dict = new Dictionary<string, bool>();
            if (pmiNumber.Length != 8)
                return dict;
            try
            {
                using (var dc = new PMSDbContext())
                {
                    if (dc.Orders.Where(i => i.PMINumber == pmiNumber).Count() > 0)
                        dict.Add("Order", true);
                    if (dc.MaterialOrderItems.Where(i => i.PMINumber == pmiNumber).Count() > 0)
                        dict.Add("MaterialOrder", true);
                    if (dc.MaterialInventoryIns.Where(i => i.PMINumber == pmiNumber).Count() > 0)
                        dict.Add("MaterialIn", true);
                    if (dc.MaterialInventoryOuts.Where(i => i.PMINumber == pmiNumber).Count() > 0)
                        dict.Add("MaterialOut", true);
                    if (dc.VHPPlans.Where(i => i.OrderID == dc.Orders.Where(o => o.PMINumber == pmiNumber).FirstOrDefault().ID).Count() > 0)
                        dict.Add("VHPPlan", true);
                    if (dc.RecordMillings.Where(i => i.PMINumber == pmiNumber).Count() > 0)
                        dict.Add("RecordMilling", true);
                    if (dc.RecordMachines.Where(i => i.PMINumber == pmiNumber).Count() > 0)
                        dict.Add("RecordMachine", true);
                    if (dc.RecordDeMolds.Where(i => i.PMINumber == pmiNumber).Count() > 0)
                        dict.Add("RecordDeMold", true);
                    if (dc.RecordTests.Where(i => i.PMINumber == pmiNumber).Count() > 0)
                        dict.Add("RecordTest", true);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

            return dict;
        }
    }
}