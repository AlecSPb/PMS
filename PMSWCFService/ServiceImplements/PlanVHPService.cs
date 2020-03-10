using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSDAL;
using PMSCommon;
using AuthorizationChecker;

namespace PMSWCFService
{
    public partial class PMSService : IPlanVHPService
    {
        public int AddVHPPlan(DcPlanVHP model)
        {
            try
            {                //自动生成搜索号
                model.SearchCode = $"{model.PlanDate.ToString("yyMMdd")}-{model.VHPDeviceCode}";
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcPlanVHP, PMSPlanVHP>());
                    var mapper = config.CreateMapper();
                    var plan = mapper.Map<PMSPlanVHP>(model);
                    dc.VHPPlans.Add(plan);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        public int AddVHPPlanByUID(DcPlanVHP model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    SaveHistory(model, uid);
                    return AddVHPPlan(model);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        public int DeleteVHPPlan(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var plan = dc.VHPPlans.Find(id);
                    if (plan != null)
                    {
                        plan.State = VHPPlanState.作废.ToString();
                        result = dc.SaveChanges();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        public int GetPlanCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = from p in dc.VHPPlans
                                 where p.State != VHPPlanState.作废.ToString()
                                 select p;
                    return result.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        public List<DcPlanVHP> GetVHPPlansByOrderID(Guid id)
        {
            Checker.CheckIfCanRun();
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<PMSPlanVHP, DcPlanVHP>());
                    var result = from p in dc.VHPPlans
                                 where p.OrderID == id && p.State != VHPPlanState.作废.ToString()
                                 orderby p.PlanDate descending, p.PlanLot, p.VHPDeviceCode
                                 select p;
                    var plans = Mapper.Map<List<PMSPlanVHP>, List<DcPlanVHP>>(result.ToList());
                    return plans;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        public int UpdateVHPPlan(DcPlanVHP model)
        {
            try
            {
                //自动生成搜索号
                model.SearchCode = $"{model.PlanDate.ToString("yyMMdd")}-{model.VHPDeviceCode}";
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcPlanVHP, PMSPlanVHP>());
                    var mapper = config.CreateMapper();
                    var plan = mapper.Map<PMSPlanVHP>(model);
                    dc.Entry(plan).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        public int UpdateVHPPlanByUID(DcPlanVHP model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    SaveHistory(model, uid);
                    return UpdateVHPPlan(model);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                return 0;
            }
        }

        private void SaveHistory(DcPlanVHP model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcPlanVHP, PMSPlanVHPHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<PMSPlanVHPHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.VHPPlanHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
            }
        }


    }
}