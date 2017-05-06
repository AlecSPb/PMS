using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSCommon;

namespace PMSWCFService
{
    public class LargeScreenService : ILargeScreenService
    {
        public List<DcRecordBonding> GetBondingUnComplete()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.RecordBondings
                                where i.State == PMSCommon.BondingState.未完成.ToString()
                                orderby i.CreateTime descending
                                select i;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordBonding, DcRecordBonding>());
                    return Mapper.Map<List<RecordBonding>, List<DcRecordBonding>>(query.ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetBondingUnCompleteStatistic()
        {
            try
            {
                var result = new List<DcStatistic>();
                var dc = new PMSDbContext();
                var query = from i in dc.RecordBondings
                            where i.State == PMSCommon.BondingState.未完成.ToString()
                            orderby i.CreateTime descending
                            select i;
                result.Add(new DcStatistic { Key = "UnCompletedRecordBonding", Value = query.Count() });
                dc.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanExtra> GetPlanByDate(DateTime planDate)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var dateStart = planDate.Date;
                    var dateEnd = planDate.Date.AddDays(1).Date;
                    var queryPlan = from p in dc.VHPPlans
                                    where p.State == PMSCommon.VHPPlanState.已核验.ToString()
                                    && p.PlanDate >= dateStart && p.PlanDate < dateEnd
                                    orderby p.PlanDate descending
                                    select p;
                    var queryResult = from p in queryPlan
                                      join o in dc.Orders
                                      on p.OrderID equals o.ID
                                      select new PMSPlanExtraModel() { Plan = p, Misson = o };
                    var final = queryResult.ToList();
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanExtraModel, DcPlanExtra>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var result = Mapper.Map<List<PMSPlanExtraModel>, List<DcPlanExtra>>(final);

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetPlanStatistic()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    List<DcStatistic> result = new List<DcStatistic>();
                    var now = DateTime.Now.Date.AddDays(1);
                    var count = (from i in dc.VHPPlans
                                 where i.State == PMSCommon.VHPPlanState.已核验.ToString()
                                 && i.PlanDate < now
                                 select i).Count();
                    result.Add(new DcStatistic { Key = "FinishedPlan", Value = count });


                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

    }
}