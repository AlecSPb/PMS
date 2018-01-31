using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSDAL;

namespace PMSWCFService
{
    public class ComplexQueryService 
    {
        public Dictionary<string, bool> GetCurrentOrderStatus(string pmiNumber)
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