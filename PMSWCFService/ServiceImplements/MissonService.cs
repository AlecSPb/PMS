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

                var result = dc.Orders.Include("PlanVHPs").Where(o => o.PolicyType.Contains("VHP") && o.State !=(int)ModelState.Deleted)
                    .OrderByDescending(o => o.CreateTime).ToList();
                var missons = Mapper.Map<List<PMSOrder>, List<DcOrder>>(result);

                return missons;
            }
        }

        public int GetMissonCountBySearch()
        {
            using (var dc=new PMSDbContext())
            {
                return dc.Orders.Where(o => o.PolicyType.Contains("VHP") && o.State != (int)ModelState.Deleted).Count();
            }
        }
    }
}