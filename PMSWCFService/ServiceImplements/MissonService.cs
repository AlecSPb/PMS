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
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PMSOrder, DcOrder>();
                    cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                });

                var result = dc.Orders.Where(o => o.PolicyType.Contains("VHP")
                && o.State != OrderState.Deleted.ToString())
                    .OrderByDescending(o => o.CreateTime).Skip(skip).Take(take).ToList();
                var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result);

                return missons;
            }
        }

        public int GetMissonCountBySearch()
        {
            using (var dc = new PMSDbContext())
            {
                return dc.Orders.Where(o => o.PolicyType.Contains("VHP") && o.State != OrderState.Deleted.ToString()).Count();
            }
        }

        public List<DcPlanVHP> GetPlansByOrderID(Guid orderid)
        {
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<PMSPlanVHP, DcPlanVHP>());
                var result = dc.VHPPlans.Where(p => p.OrderID == orderid && p.State != OrderState.Deleted.ToString())
                    .OrderByDescending(p => p.CreateTime).ToList();
                var plans = Mapper.Map<List<PMSPlanVHP>, List<DcPlanVHP>>(result);
                return plans;
            }
        }
    }
}