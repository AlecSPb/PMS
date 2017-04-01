using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSWCFService.DataContracts;
using PMSDAL;
using AutoMapper;
using PMSCommon;

namespace PMSWCFService
{
    public partial class PMSService : IMissonService
    {
        public List<DcOrder> GetMissonBySearchInPage(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = from o in dc.Orders
                                 where o.PolicyType.Contains("VHP") && o.State != OrderState.Deleted.ToString() && o.State != OrderState.UnChecked.ToString()
                                 orderby o.CreateTime descending
                                 select o;

                    var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result.Skip(skip).Take(take).ToList());

                    return missons;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int GetMissonCountBySearch()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.Orders.Where(o => o.PolicyType.Contains("VHP") && o.State != OrderState.Deleted.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DcPlanVHP> GetPlansByOrderID(Guid orderid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<PMSPlanVHP, DcPlanVHP>());
                    var result = from p in dc.VHPPlans
                                  where p.OrderID == orderid && p.State != OrderState.Deleted.ToString()
                                  orderby p.PlanDate descending
                                  select p;
                    var plans = Mapper.Map<List<PMSPlanVHP>, List<DcPlanVHP>>(result.ToList());
                    return plans;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}