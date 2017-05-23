using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;

namespace PMSWCFService
{
    public partial class ExtraService : IPlanVHPConclusionService
    {
        public int AddPlanVHPConclusion(DcPlanVHPConclusion model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcPlanVHPConclusion, PlanVHPConclusion>());
                    var entity = Mapper.Map<PlanVHPConclusion>(model);
                    dc.PlanVHPConclusions.Add(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeletePlanVHPConclusion(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var entity = dc.PlanVHPConclusions.Find(id);
                    dc.PlanVHPConclusions.Remove(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcPlanVHPConclusionExtra> GetPlanVHPConclusionExtra(int s, int t, string composition, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<PMSPlanVHPConclusionExtra, DcPlanVHPConclusionExtra>();
                        cfg.CreateMap<PlanVHPConclusion, DcPlanVHPConclusion>();
                        cfg.CreateMap<PMSOrder, DcOrder>();
                        cfg.CreateMap<PMSPlanVHP, DcPlanVHP>();
                    });

                    var query = from pc in dc.PlanVHPConclusions
                                from o in dc.Orders
                                from p in dc.VHPPlans
                                where o.ID == p.OrderID && p.ID == pc.PlanID
                                orderby pc.CreateTime descending, pc.Grade
                                select new PMSPlanVHPConclusionExtra
                                {
                                    Order = o,
                                    PlanVHP = p,
                                    Conclusion = pc
                                };
                    return Mapper.Map<List<PMSPlanVHPConclusionExtra>, List<DcPlanVHPConclusionExtra>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetPlanVHPConclusionExtraCount(string composition, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from pc in dc.PlanVHPConclusions
                                from o in dc.Orders
                                from p in dc.VHPPlans
                                where o.ID == p.OrderID && p.ID == pc.PlanID
                                select new PMSPlanVHPConclusionExtra
                                {
                                    Order = o,
                                    PlanVHP = p,
                                    Conclusion = pc
                                };

                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdatePlanVHPConclusion(DcPlanVHPConclusion model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcPlanVHPConclusion, PlanVHPConclusion>());
                    var entity = Mapper.Map<PlanVHPConclusion>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    return dc.SaveChanges();
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