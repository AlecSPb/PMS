using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSCommon;
using System.Data.Entity;

namespace PMSWCFService
{
    public class LargeScreenService : ILargeScreenService
    {
        public List<DcRecordBonding> GetBondingUnComplete(int s, int t)
        {
            try
            {
                XS.Run();
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.RecordBondings
                                where i.State == PMSCommon.BondingState.未完成.ToString()
                                orderby DbFunctions.TruncateTime(i.CreateTime) descending,
                                    i.PlanBatchNumber, i.TargetProductID
                                select i;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordBonding, DcRecordBonding>());
                    return Mapper.Map<List<RecordBonding>, List<DcRecordBonding>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        public int GetBondingUnCompleteCount()
        {
            try
            {
                XS.Run();
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.RecordBondings
                                where i.State == PMSCommon.BondingState.未完成.ToString()
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        public List<DcStatistic> GetBondingCompleteStatistic()
        {
            try
            {
                XS.Run();
                var result = new List<DcStatistic>();
                var dc = new PMSDbContext();
                var query = from i in dc.RecordBondings
                            where i.State == PMSCommon.BondingState.完成.ToString()
                            orderby i.CreateTime descending
                            select i;
                result.Add(new DcStatistic { Key = "CompletedRecordBondingCount", Value = query.Count() });
                dc.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanExtra> GetPlanByDate(DateTime planDate)
        {
            try
            {
                XS.Run();
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
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanExtra> GetPlanByDateDeviceCode(int planlot, DateTime planDate, string deviceCode)
        {
            try
            {
                XS.Run();
                using (var dc = new PMSDbContext())
                {
                    var today = planDate.Date;
                    var queryPlan = from p in dc.VHPPlans
                                    where p.State == PMSCommon.VHPPlanState.已核验.ToString()
                                    && p.PlanLot == planlot
                                    && p.VHPDeviceCode.Contains(deviceCode)
                                    && DbFunctions.TruncateTime(p.PlanDate) == today
                                    orderby p.PlanDate descending
                                    select p;
                    //连接订单信息
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
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcStatistic> GetPlanStatistic()
        {
            try
            {
                XS.Run();
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
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordMilling> GetRecordMillings(DateTime planDate)
        {
            try
            {
                XS.Run();
                using (var dc = new PMSDbContext())
                {
                    List<DcRecordMilling> result = new List<DcRecordMilling>();

                    string todayString = planDate.ToString("yyMMdd");
                    var query = from m in dc.RecordMillings
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                && m.VHPPlanLot.Contains(todayString)
                                orderby m.PlanBatchNumber, m.VHPPlanLot
                                select m;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordMilling, DcRecordMilling>());

                    return Mapper.Map<List<RecordMilling>, List<DcRecordMilling>>(query.ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanWithMisson> GetPlanOfMachine()
        {
                XS.Run();
            using (var dc = new PMSDAL.PMSDbContext())
            {
                var queryPlan = (from p in dc.VHPPlans
                                 where p.State != PMSCommon.CommonState.作废.ToString()
                                 && p.PlanType == "加工"
                                 orderby p.PlanDate descending
                                 select p).Take(20);
                var queryResult = from p in queryPlan
                                  join o in dc.Orders
                                  on p.OrderID equals o.ID
                                  select new PMSPlanWithMissonModel() { Plan = p, Misson = o };
                var final = queryResult.ToList();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<PMSPlanWithMissonModel, DcPlanWithMisson>();
                    cfg.CreateMap<PMSOrder, DcOrder>();
                    cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                });

                var result = Mapper.Map<List<PMSPlanWithMissonModel>, List<DcPlanWithMisson>>(final);

                return result;
            }
        }
    }
}